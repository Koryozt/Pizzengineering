using Pizzengineering.Domain.Entities;

namespace Pizzengineering.Application.Orders.Queries;

public sealed record OrderResponse(
	Guid Id,
	string Name,
	User User,
	ICollection<Pizza> Pizzas,
	DateTime PurchasedAtUtc);
