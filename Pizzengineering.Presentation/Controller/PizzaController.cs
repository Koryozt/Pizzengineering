using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pizzengineering.Application.Pizzas.Commands.Create;
using Pizzengineering.Application.Pizzas.Queries;
using Pizzengineering.Application.Pizzas.Queries.All;
using Pizzengineering.Application.Pizzas.Queries.ById;
using Pizzengineering.Application.Pizzas.Queries.GetByCondition;
using Pizzengineering.Domain.Shared;
using Pizzengineering.Domain.ValueObjects.User;
using Pizzengineering.Presentation.Abstractions;

namespace Pizzengineering.Presentation.Controller;

[Route("api/[controller]/[action]/")]
public sealed class PizzaController : ApiController
{
	public PizzaController(ISender sender) : base(sender)
	{

	}

	[HttpPost]
	public async Task<IActionResult> Create(CreatePizzaCommand command, CancellationToken cancellationToken)
	{
		Result<Guid> result = await Sender.Send(command, cancellationToken);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Created(Request.Path, result.Value);
	}

	[HttpGet]
	public async Task<IActionResult> All(GetAllPizzasQuery query, CancellationToken cancellationToken)
	{
		Result<List<PizzaResponse>> result = await Sender.Send(query, cancellationToken);

		return Ok(result.Value);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
	{
		GetPizzaByIdQuery query = new(id);

		Result<PizzaResponse> result = await Sender.Send(query, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
	}

	[HttpGet("{name:string}")]
	public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
	{
		Result<Name> nameResult = Name.Create(name);

		if (nameResult.IsFailure)
		{
			return BadRequest(nameResult.Error);
		}

		GetPizzaByNameQuery query = new(nameResult.Value);
		Result<PizzaResponse> result = await Sender.Send(query, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
	}
}
