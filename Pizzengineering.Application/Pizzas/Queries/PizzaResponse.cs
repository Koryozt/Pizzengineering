using Pizzengineering.Domain.ValueObjects.Pizza;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Pizzas.Queries;

public sealed record PizzaResponse(
	Guid Id,
	Name Name,
	Description Description,
	Rate Rate,
	double Price,
	bool HasDiscount
	);
