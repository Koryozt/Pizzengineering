using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Pizzas.Queries.GetByCondition;

public sealed record GetPizzaByNameQuery(Name Name) : IQuery<PizzaResponse>;
