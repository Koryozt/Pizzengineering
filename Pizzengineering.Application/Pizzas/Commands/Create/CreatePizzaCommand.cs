using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Domain.ValueObjects.Pizza;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Pizzas.Commands.Create;

public sealed record CreatePizzaCommand(
	string Name,
	double Price,
	bool HasDiscount,
	double Rate,
	string Cover,
	string Description) : ICommand<Guid>;
