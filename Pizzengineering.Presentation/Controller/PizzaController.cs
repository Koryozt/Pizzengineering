using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pizzengineering.Application.Pizzas.Queries.All;
using Pizzengineering.Presentation.Abstractions;

namespace Pizzengineering.Presentation.Controller;

[Route("api/[controller]/[action]")]
public sealed class PizzaController : ApiController
{
	public PizzaController(ISender sender) : base(sender)
	{

	}

	[HttpGet]
	public async Task<IActionResult> All(GetAllPizzasQuery query, CancellationToken cancellationToken)
	{
		var result = await Sender.Send(query, cancellationToken);

		return Ok(result);
	}
}
