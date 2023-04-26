using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Users.Queries;

public sealed record UserResponse(
	Guid Id,
	Name Firstname,
	Name Lastname,
	Email Email,
	Password Password,
	PaymentInformation PaymentInformation,
	IReadOnlyCollection<Order> Orders);
