using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Application.Orders.Queries;
using Pizzengineering.Application.Payments.Queries.GetByCondition;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Application.Payments.Queries;

public sealed class PaymentQueriesHandler
	: IQueryHandler<GetPaymentByIdQuery, PaymentResponse>
{
	private readonly IPaymentInfoRepository _repository;
	private const string AddressLineTwoIfNull = "Not Available";

	public PaymentQueriesHandler(IPaymentInfoRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<PaymentResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
	{
		var info = await _repository.GetByIdAsync(request.Id, cancellationToken);

		if (info is null)
		{
			return Result.Failure<PaymentResponse>(DomainErrors.PaymentInformation.NotFound(request.Id));
		}

		var response = new PaymentResponse(
			info.Id,
			info.UserId,
			info.CardNumber,
			info.NameOnCard,
			info.ExpirationDate,
			info.AddressLineOne,
			info.AddressLineTwo ?? AddressLineTwoIfNull,
			info.Country,
			info.State,
			info.City
			);

		return Result.Success(response);
	}
}
