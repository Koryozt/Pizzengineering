using Pizzengineering.Application.Abstractions.Messaging;

namespace Pizzengineering.Application.Pizzas.Queries.All;

public sealed record GetAllPizzasQuery() : IQuery<List<PizzaResponse>>;
