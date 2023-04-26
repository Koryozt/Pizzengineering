using FluentValidation;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Users.Commands.Register;

public sealed class CreateUserCommandValidator : AbstractValidator<User>
{
	public CreateUserCommandValidator()
	{
		RuleFor(e => e.Firstname.Value)
			.NotEmpty()
			.MaximumLength(Name.MaxLength);

		RuleFor(e => e.Lastname.Value)
			.NotEmpty()
			.MaximumLength(Name.MaxLength);

		RuleFor(e => e.Email.Value)
			.NotEmpty()
			.EmailAddress()
			.MaximumLength(Email.MaxLength);

		RuleFor(e => e.Password.Value)
			.NotEmpty()
			.Matches(Password.Pattern)
			.MinimumLength(Password.MinValue)
			.MaximumLength(Password.MaxValue);

		RuleFor(e => e.PaymentInformation).NotEmpty();
	}
}
