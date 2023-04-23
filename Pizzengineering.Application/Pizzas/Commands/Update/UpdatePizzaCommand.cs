using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Domain.ValueObjects.Pizza;

namespace Pizzengineering.Application.Pizzas.Commands.Update;

public sealed record UpdatePizzaCommand(
	Guid Id, 
	Rate Rate,
	double Price,
	bool HasDiscount) : ICommand;
