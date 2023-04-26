using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Application.Orders.Queries.GetByCondition;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Application.Orders.Queries;

public sealed class OrderQueriesHandler :
	IQueryHandler<GetOrderByUserQuery, OrderResponse>,
	IQueryHandler<GetOrderByIdQuery, OrderResponse>
{
	private readonly IOrderRepository _repository;

	public OrderQueriesHandler(IOrderRepository repository)
	{
		_repository = repository;
	}

	// User
	public async Task<Result<OrderResponse>> Handle(GetOrderByUserQuery request, CancellationToken cancellationToken)
	{
		var order = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);

		if (order is null)
		{
			return Result.Failure<OrderResponse>(DomainErrors.Order.NotFound(request.UserId));
		}

		var response = new OrderResponse(
			order.Id,
			$"Order [{order.Id} - {order.UserId}]",
			order.User,
			order.Pizzas,
			order.PurchasedAtUtc);

		return Result.Success(response);
	}

	// ID
	public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
	{
		var order = await _repository.GetByIdAsync(request.Id, cancellationToken);

		if (order is null)
		{
			return Result.Failure<OrderResponse>(DomainErrors.Order.NotFound(request.Id));
		}

		var response = new OrderResponse(
			order.Id,
			$"Order [{order.Id} - {order.UserId}]",
			order.User,
			order.Pizzas,
			order.PurchasedAtUtc);

		return Result.Success(response);
	}
}
