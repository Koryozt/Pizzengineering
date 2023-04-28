using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Application.Payments.Commands.Create;
using Pizzengineering.Application.Payments.Commands.Update;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Shared;
using Pizzengineering.Domain.ValueObjects.PaymentInformation;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Payments.Commands;

public sealed class PaymentCommandsHandler :
	ICommandHandler<CreatePaymentCommand, Guid>,
	ICommandHandler<UpdatePaymentCommand>
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

		Result<CreditCardNumber> cardNumberResult = CreditCardNumber.Create(request.CreditCardNumber);
		Result<Name> nameResult = Name.Create(request.NameOnCard);

		if (cardNumberResult.IsFailure ||  nameResult.IsFailure) 
		{
			return Result.Failure<Guid>(
				DomainErrors
				.PaymentInformation
				.Invalid(request.CreditCardNumber, request.NameOnCard));
		}

		var information = PaymentInformation.Create(
			Guid.NewGuid(),
			request.User,
			cardNumberResult.Value,
			nameResult.Value,
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

	public async Task<Result> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
	{
		var information = await _repository.GetByIdAsync(request.Id, cancellationToken);

		if (information is null)
		{
			return Result.Failure(
				DomainErrors
				.PaymentInformation
				.NotFound(request.Id));
		}

		Result<CreditCardNumber> cardNumberResult = CreditCardNumber.Create(request.CreditCardNumber);
		Result<Name> nameResult = Name.Create(request.NameOnCard);

		if (cardNumberResult.IsFailure || nameResult.IsFailure)
		{
			return Result.Failure<Guid>(
				DomainErrors
				.PaymentInformation
				.Invalid(request.CreditCardNumber, request.NameOnCard));
		}

		information.ChangeData(
			cardNumberResult.Value,
			nameResult.Value,
			request.ExpirationDate,
			request.AddressLineOne,
			request.AddressLineTwo,
			request.Country,
			request.State,
			request.City);

		_repository.Update(information);
		await _uow.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}
