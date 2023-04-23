using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Application.Pizzas.Commands.Create;
using Pizzengineering.Application.Pizzas.Commands.Update;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Application.Pizzas.Commands;

public sealed class PizzaCommandsHandler : 
	ICommandHandler<CreatePizzaCommand, Guid>,
	ICommandHandler<UpdatePizzaCommand>
{
	private readonly IPizzaRepository _repository;
	private readonly IUnitOfWork _uow;

	public PizzaCommandsHandler(IPizzaRepository repository, IUnitOfWork uow)
	{
		_repository = repository;
		_uow = uow;
	}

	public async Task<Result<Guid>> Handle(CreatePizzaCommand request, CancellationToken cancellationToken)
	{
		var pizza = Pizza.Create(
			Guid.NewGuid(),
			request.Name,
			request.Description,
			request.Rate,
			request.Price,
			request.HasDiscount
			);

		await _repository.AddAsync(pizza, cancellationToken);
		await _uow.SaveChangesAsync(cancellationToken);

		return Result.Success(pizza.Id);
	}

	public async Task<Result> Handle(UpdatePizzaCommand request, CancellationToken cancellationToken)
	{
		var pizza = await _repository.GetByIdAsync(request.Id, cancellationToken);

		if (pizza is null)
		{
			return Result.Failure(DomainErrors.Pizza.NotFound);
		}

		pizza.ChangeVariableInformation();
	}
}
