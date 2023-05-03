using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pizzengineering.Application.Pizzas.Commands.Create;
using Pizzengineering.Application.Pizzas.Commands.Update;
using Pizzengineering.Application.Pizzas.Queries;
using Pizzengineering.Application.Pizzas.Queries.All;
using Pizzengineering.Application.Pizzas.Queries.ById;
using Pizzengineering.Application.Pizzas.Queries.GetByCondition;
using Pizzengineering.Domain.Enumerators;
using Pizzengineering.Domain.Shared;
using Pizzengineering.Domain.ValueObjects.User;
using Pizzengineering.Infrastructure.Authentication;
using Pizzengineering.Presentation.Abstractions;

namespace Pizzengineering.Presentation.Controller;

[Route("api/[controller]/[action]/")]
public sealed class PizzaController : ApiController
{
	public PizzaController(ISender sender) : base(sender)
	{

	}

	[HttpPost]
	[HasPermission(Permissions.Administration)]
	[HasPermission(Permissions.AccessUser)]
	[HasPermission(Permissions.CreatePizza)]
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
	public async Task<IActionResult> All(CancellationToken cancellationToken)
	{
		GetAllPizzasQuery query = new();
		Result<List<PizzaResponse>> result = await Sender.Send(query, cancellationToken);

		return Ok(result.Value);
	}

	[HttpGet("{id:guid}")]
	[HasPermission(Permissions.AccessUser)]
	[HasPermission(Permissions.ReadPizza)]
	public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
	{
		GetPizzaByIdQuery query = new(id);

		Result<PizzaResponse> result = await Sender.Send(query, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
	}

	[HttpGet]
	public async Task<IActionResult> GetByName([FromQuery] string name, CancellationToken cancellationToken)
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

	[HttpPut]
	[HasPermission(Permissions.AccessUser)]
	[HasPermission(Permissions.UpdatePizza)]
	public async Task<IActionResult> Update(UpdatePizzaCommand command, CancellationToken cancellationToken)
	{
		Result result = await Sender.Send(command, cancellationToken);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return NoContent();
	}
	
}
