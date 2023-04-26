using Pizzengineering.Application.Abstractions.Messaging;

namespace Pizzengineering.Application.Payments.Queries.GetByCondition;

public sealed record GetPaymentByIdQuery(
	Guid Id) : IQuery<PaymentResponse>;
