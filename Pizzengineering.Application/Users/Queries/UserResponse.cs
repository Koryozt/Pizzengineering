using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
