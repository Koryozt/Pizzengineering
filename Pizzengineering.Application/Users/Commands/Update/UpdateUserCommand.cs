using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Users.Commands.Update;

public sealed record UpdateUserCommand(
	Guid Id,
	Name Firstname,
	Name Lastname) : ICommand;
