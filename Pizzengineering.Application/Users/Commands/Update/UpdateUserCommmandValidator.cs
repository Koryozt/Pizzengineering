using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Users.Commands.Update;

public class UpdateUserCommmandValidator : AbstractValidator<User>
{
	public UpdateUserCommmandValidator()
	{
		RuleFor(e => e.Id)
			.NotEmpty();

		RuleFor(e => e.Firstname.Value)
			.NotEmpty()
			.MaximumLength(Name.MaxLength);

		RuleFor(e => e.Lastname.Value)
			.NotEmpty()
			.MaximumLength(Name.MaxLength);
	}
}
