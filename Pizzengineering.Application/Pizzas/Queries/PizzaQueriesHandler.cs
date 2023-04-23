using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Application.Pizzas.Queries;
using Pizzengineering.Application.Pizzas.Queries.All;
using Pizzengineering.Application.Pizzas.Queries.ById;
using Pizzengineering.Application.Pizzas.Queries.GetByCondition;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Application.Pizzas.Queries;

public sealed class PizzaQueriesHandler :
	IQueryHandler<GetAllPizzasQuery, List<PizzaResponse>>,
	IQueryHandler<GetPizzaByIdQuery, PizzaResponse>,
	IQueryHandler<GetPizzaByNameQuery, PizzaResponse>
{
	private readonly IPizzaRepository _repository;

	public PizzaQueriesHandler(IPizzaRepository repository)
	{
		_repository = repository;
	}

	// By Name
	public async Task<Result<PizzaResponse>> Handle(GetPizzaByNameQuery request, CancellationToken cancellationToken)
	{
		var pizza = await _repository.GetByNameAsync(request.Name, cancellationToken);

		if (pizza is null)
		{
			return Result.Failure<PizzaResponse>(
				DomainErrors
				.Pizza
				.NotFound);
		}

		var response = new PizzaResponse(
			pizza.Id,
			pizza.Name,
			pizza.Description,
			pizza.Rate,
			pizza.Price,
			pizza.HasDiscount);

		return Result.Success(response);
	}

	// By Id
	public async Task<Result<PizzaResponse>> Handle(GetPizzaByIdQuery request, CancellationToken cancellationToken)
	{
		var pizza = await _repository.GetByIdAsync(request.Id, cancellationToken);

		if (pizza is null)
		{
			return Result.Failure<PizzaResponse>(
				DomainErrors
				.Pizza
				.NotFound);
		}

		var response = new PizzaResponse(
			pizza.Id,
			pizza.Name,
			pizza.Description,
			pizza.Rate,
			pizza.Price,
			pizza.HasDiscount);

		return Result.Success(response);
	}

	// All
	public async Task<Result<List<PizzaResponse>>> Handle(GetAllPizzasQuery request, CancellationToken cancellationToken)
	{
		var pizzas = await _repository.GetAllAsync(cancellationToken);

		List<PizzaResponse> pizzaResponses = new();

		pizzas.ForEach(e =>
		{
			var response = new PizzaResponse(
			e.Id,
			e.Name,
			e.Description,
			e.Rate,
			e.Price,
			e.HasDiscount);

			pizzaResponses.Add(response);
		});

		return Result.Success(pizzaResponses);
	}
}
