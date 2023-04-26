using Pizzengineering.Application.Abstractions.Messaging;

namespace Pizzengineering.Application.Pizzas.Queries.ById;

public sealed record GetPizzaByIdQuery(Guid Id) : IQuery<PizzaResponse>;
