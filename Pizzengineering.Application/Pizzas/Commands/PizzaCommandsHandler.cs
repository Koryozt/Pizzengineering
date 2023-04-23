using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Application.Pizzas.Commands.Create;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Application.Pizzas.Commands;

public sealed class PizzaCommandsHandler : ICommandHandler<CreatePizzaCommand, Guid>
{
	public Task<Result<Guid>> Handle(CreatePizzaCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
