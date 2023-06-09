﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.Pizza;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Pizzas.Commands.Create;

public sealed class UpdatePizzaCommandValidator	: AbstractValidator<Pizza>
{
	public UpdatePizzaCommandValidator()
	{
		RuleFor(e => e.Name.Value)
			.NotEmpty()
			.MaximumLength(Name.MaxLength);

		RuleFor(e => e.Price)
			.GreaterThanOrEqualTo(1.0)
			.LessThanOrEqualTo(100.0);

		RuleFor(e => e.HasDiscount)
			.NotEmpty();

		RuleFor(e => e.Description.Value)
			.NotEmpty()
			.MaximumLength(Description.MaxValue);

		RuleFor(e => e.Rate.Value)
			.NotEmpty()
			.GreaterThanOrEqualTo(Rate.MinValue)
			.LessThanOrEqualTo(Rate.MaxValue);
	}
}
