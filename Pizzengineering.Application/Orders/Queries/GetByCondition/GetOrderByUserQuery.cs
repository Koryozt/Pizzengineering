using Pizzengineering.Application.Abstractions.Messaging;

namespace Pizzengineering.Application.Orders.Queries.GetByCondition;

public sealed record GetOrderByUserQuery(Guid UserId) : IQuery<OrderResponse>;