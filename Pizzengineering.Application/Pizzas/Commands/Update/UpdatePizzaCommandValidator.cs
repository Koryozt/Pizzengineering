using FluentValidation;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.Pizza;

namespace Pizzengineering.Application.Pizzas.Commands.Update;

public sealed class UpdatePizzaCommandValidator : AbstractValidator<Pizza>
{
	public UpdatePizzaCommandValidator()
	{
		RuleFor(e => e.Price)
			.GreaterThanOrEqualTo(1.0)
			.LessThanOrEqualTo(100.0);

		RuleFor(e => e.HasDiscount)
			.NotEmpty();

		RuleFor(e => e.Rate.Value)
			.NotEmpty()
			.GreaterThanOrEqualTo(Rate.MinValue)
			.LessThanOrEqualTo(Rate.MaxValue);
	}
}
