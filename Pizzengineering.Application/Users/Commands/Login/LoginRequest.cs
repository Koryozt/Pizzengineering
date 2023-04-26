using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Users.Commands.Login;

public sealed record LoginRequest(
	Email Email,
	Password Password);
