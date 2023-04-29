using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.ValueObjects.Pizza;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Pizzas.Queries;

public sealed record PizzaResponse(
	Guid Id,
	Name Name,
	Description Description,
	Rate Rate,
	string cover,
	double Price,
	bool HasDiscount
	);
