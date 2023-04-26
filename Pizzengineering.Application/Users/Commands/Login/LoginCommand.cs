using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Users.Commands.Login;

public sealed record LoginCommand(
	Email Email,
	Password Password) : ICommand<string>;
