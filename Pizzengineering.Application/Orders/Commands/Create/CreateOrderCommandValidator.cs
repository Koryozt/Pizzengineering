using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Pizzengineering.Domain.Entities;

namespace Pizzengineering.Application.Orders.Commands.Create;

public sealed class CreateOrderCommandValidator : AbstractValidator<Order>
{
	public CreateOrderCommandValidator()
	{
		RuleFor(e => e.User)
			.NotEmpty()
			.NotNull();

		RuleFor(e => e.Pizzas)
			.NotEmpty();

		RuleFor(e => e.PurchasedAtUtc)
			.NotEmpty()
			.ExclusiveBetween(DateTime.UtcNow, DateTime.MaxValue);
	}
}
