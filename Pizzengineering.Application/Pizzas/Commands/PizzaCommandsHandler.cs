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
using Pizzengineering.Domain.ValueObjects.Pizza;
using Pizzengineering.Domain.ValueObjects.User;

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
		Result<Name> nameResult = Name.Create(request.Name);
		Result<Rate> rateResult = Rate.Create(request.Rate);
		Result<Description> descResult = Description.Create(request.Description);

		if (nameResult.IsFailure || rateResult.IsFailure || descResult.IsFailure)
		{
			return Result.Failure<Guid>(
				DomainErrors
				.Pizza
				.Invalid(request.Name, request.Rate, request.Description));
		}

		var pizza = Pizza.Create(
			Guid.NewGuid(),
			nameResult.Value,
			descResult.Value,
			rateResult.Value,
			request.Cover,
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

		Result<Rate> rateResult = Rate.Create(request.Rate);

		if (rateResult.IsFailure)
		{
			return Result.Failure<Guid>(
				DomainErrors
				.Pizza
				.Invalid(pizza.Name.Value, request.Rate, pizza.Description.Value));
		}

		pizza.ChangeVariableInformation(
			rateResult.Value,
			request.Price,
			request.HasDiscount);

		_repository.Update(pizza);
		await _uow.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}
