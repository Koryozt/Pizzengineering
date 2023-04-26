using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Users.Commands.Register;

public sealed record RegisterRequest(
	Name Firstname,
	Name Lastname,
	Email Email,
	Password Password,
	PaymentInformation PaymentInformation);