using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pizzengineering.Application.Payments.Commands.Create;
using Pizzengineering.Application.Payments.Commands.Update;
using Pizzengineering.Application.Payments.Queries;
using Pizzengineering.Application.Payments.Queries.GetByCondition;
using Pizzengineering.Domain.Enumerators;
using Pizzengineering.Domain.Shared;
using Pizzengineering.Infrastructure.Authentication;
using Pizzengineering.Presentation.Abstractions;

namespace Pizzengineering.Presentation.Controller;

[Route("api/[controller]/[action]")]
public sealed class PaymentInformationController : ApiController
{
	public PaymentInformationController(ISender sender) : base(sender)
	{
	}

	[HttpPost]
	[HasPermission(Permissions.AccessUser)]
	[HasPermission(Permissions.CreatePaymentInformation)]
	public async Task<IActionResult> Create(
		CreatePaymentCommand command, 
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
	[HasPermission(Permissions.ReadPaymentInformation)]
	public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
	{
		GetPaymentByIdQuery query = new(id);

		Result<PaymentResponse> result = await Sender.Send(query, cancellationToken);

		if (result.IsFailure)
		{
			return NotFound(result.Error);
		}

		return Ok(result.Value);
	}

	[HttpPut]
	[HasPermission(Permissions.AccessUser)]
	[HasPermission(Permissions.UpdatePaymentInformation)]
	public async Task<IActionResult> Update(UpdatePaymentCommand command, CancellationToken cancellationToken)
	{
		Result result = await Sender.Send(command, cancellationToken);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return NoContent();
	}
}
