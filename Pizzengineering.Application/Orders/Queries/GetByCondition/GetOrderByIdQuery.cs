using Pizzengineering.Application.Abstractions.Messaging;

namespace Pizzengineering.Application.Orders.Queries.GetByCondition;

public sealed record GetOrderByIdQuery(Guid Id) : IQuery<OrderResponse>;
