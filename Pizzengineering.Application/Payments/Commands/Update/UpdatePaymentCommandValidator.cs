using FluentValidation;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.PaymentInformation;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Payments.Commands.Update;

public sealed class UpdatePaymentCommandValidator : AbstractValidator<PaymentInformation>
{
	public UpdatePaymentCommandValidator()
	{
		RuleFor(e => e.CardNumber.Value)
			.NotEmpty()
			.Matches(CreditCardNumber.Pattern)
			.Must(e => CreditCardNumber.IsCardNumberValid(e));

		RuleFor(e => e.NameOnCard.Value)
			.NotEmpty()
			.MaximumLength(Name.MaxLength);

		RuleFor(e => e.ExpirationDate)
			.NotEmpty()
			.GreaterThanOrEqualTo(DateTime.UtcNow);

		RuleFor(e => e.AddressLineOne)
			.NotEmpty();

		RuleFor(e => e.Country)
			.NotEmpty();

		RuleFor(e => e.State)
			.NotEmpty();

		RuleFor(e => e.City)
			.NotEmpty();
	}
}
