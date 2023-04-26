using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;

namespace Pizzengineering.Application.Pizzas.Queries.All;

public sealed record GetAllPizzasQuery() : IQuery<List<PizzaResponse>>;
