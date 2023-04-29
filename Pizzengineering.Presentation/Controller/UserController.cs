using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Pizzengineering.Application.Users.Commands.Login;
using Pizzengineering.Application.Users.Commands.Register;
using Pizzengineering.Application.Users.Commands.Update;
using Pizzengineering.Application.Users.Queries;
using Pizzengineering.Application.Users.Queries.GetUserById;
using Pizzengineering.Domain.Shared;
using Pizzengineering.Presentation.Abstractions;

namespace Pizzengineering.Presentation.Controller;

[Route("api/[controller]/[action]/")]
public sealed class UserController : ApiController
{
	public UserController(ISender sender) : base(sender)
	{
	}

	[HttpPost]
	public async Task<IActionResult> Register(
		[FromBody] RegisterRequest request, 
		CancellationToken cancellationToken)
	{
		CreateUserCommand command = new(
			request.Firstname,
			request.Lastname,
			request.Email,
			request.Password);

		Result<Guid> result = await Sender.Send(command, cancellationToken);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return Created(Request.Path, result.Value);
	}

	[HttpPost] 
	public async Task<IActionResult> Login(
		[FromBody] LoginRequest request, 
		CancellationToken cancellationToken)
	{
		LoginCommand command = new(
			request.Email,
			request.Password);

		Result<string> result = await Sender.Send(command, cancellationToken);

		if (result.IsFailure) 
		{
			return Unauthorized(result.Error);
		}

		return Ok(result.Value);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> Get(
		Guid id,
		CancellationToken cancellationToken)
	{
		GetUserByIdQuery query = new(id);

		Result<UserResponse> response = await Sender.Send(query,cancellationToken);

		return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
	}

	[HttpGet]
	public async Task<IActionResult> Me(CancellationToken cancellationToken)
	{
		var userId = HttpContext.User.Claims.First(e => e.Type == ClaimTypes.NameIdentifier).Value;

		var id = Guid.Parse(userId);

		GetUserByIdQuery query = new(id);

		Result<UserResponse> response = await Sender.Send(query, cancellationToken);

		return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
	}

	[HttpPatch]
	public async Task<IActionResult> Update(
		UpdateUserCommand command, 
		CancellationToken cancellationToken)
	{
		Result result = await Sender.Send(command, cancellationToken);

		if (result.IsFailure)
		{
			return BadRequest(result.Error);
		}

		return NoContent();
	}
}
