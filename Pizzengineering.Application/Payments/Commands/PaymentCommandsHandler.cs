using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Application.Payments.Commands.Create;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Shared;
using Pizzengineering.Domain.ValueObjects.PaymentInformation;

namespace Pizzengineering.Application.Payments.Commands;

public sealed class PaymentCommandsHandler :
	ICommandHandler<CreatePaymentCommand, Guid>
{
	private readonly IPaymentInfoRepository _repository;
	private readonly IUnitOfWork _uow;

	public PaymentCommandsHandler(IPaymentInfoRepository repository, IUnitOfWork uow)
	{
		_repository = repository;
		_uow = uow;
	}

	public async Task<Result<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
	{
		if (request.User.PaymentInformation is not null)
		{
			return Result.Failure<Guid>(
				DomainErrors
				.PaymentInformation
				.UserAlreadyWithPaymentInformation(request.User.Id));
		}

		if (CreditCardNumber.IsCardNumberValid(request.CreditCardNumber.Value))
		{
			return Result.Failure<Guid>(
				DomainErrors
				.CreditCardNumber
				.Invalid);
		}

		var information = PaymentInformation.Create(
			Guid.NewGuid(),
			request.User,
			request.CreditCardNumber,
			request.NameOnCard,
			request.ExpirationDate,
			request.AddressLineOne,
			request.AddressLineTwo,
			request.Country,
			request.State,
			request.City);

		await _repository.AddAsync(information, cancellationToken);
		await _uow.SaveChangesAsync(cancellationToken);

		return Result.Success(information.Id);
	}
}
