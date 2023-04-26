﻿using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Users.Commands.Register;

public sealed record CreateUserCommand(
	Name Firstname,
	Name Lastname,
	Email Email,
	Password Password,
	PaymentInformation PaymentInformation) : ICommand<Guid>;
