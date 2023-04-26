using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Domain.ValueObjects.Pizza;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Pizzas.Commands.Create;

public sealed record CreatePizzaCommand(
	Name Name,
	double Price,
	bool HasDiscount,
	Rate Rate,
	Description Description) : ICommand<Guid>;
