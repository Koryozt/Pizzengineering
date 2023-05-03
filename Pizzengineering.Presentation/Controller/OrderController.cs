using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pizzengineering.Application.Orders.Commands.Create;
using Pizzengineering.Application.Orders.Queries;
using Pizzengineering.Application.Orders.Queries.GetByCondition;
using Pizzengineering.Domain.Enumerators;
using Pizzengineering.Domain.Shared;
using Pizzengineering.Infrastructure.Authentication;
using Pizzengineering.Presentation.Abstractions;

namespace Pizzengineering.Presentation.Controller;

[Route("api/[controller]/[action]/")]
public sealed class OrderController : ApiController
{
	public OrderController(ISender sender) : base(sender)
	{
	}

	[HttpPost]
	[HasPermission(Permissions.AccessUser)]
	[HasPermission(Permissions.CreateOrder)]
	public async Task<IActionResult> Create(
		[FromBody] CreateOrderCommand command, 
		CancellationToken cancellationToken)
	{
		Result<Guid> result = await Sender.Send(command, cancellationToken);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Created(Request.Path, result.Value);
	}

	[HttpGet("{id:guid}")]
	[HasPermission(Permissions.AccessUser)]
	[HasPermission(Permissions.Administration)]
	public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
	{
		GetOrderByIdQuery query = new(id);

		Result<OrderResponse> result = await Sender.Send(query, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
	}

	[HttpGet("{id:guid}")]
	[HasPermission(Permissions.AccessUser)]
	public async Task<IActionResult> GetByUser(Guid id, CancellationToken cancellationToken)
	{
		GetOrderByUserQuery query = new(id);

		Result<OrderResponse> result = await Sender.Send(query, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
	}
}
