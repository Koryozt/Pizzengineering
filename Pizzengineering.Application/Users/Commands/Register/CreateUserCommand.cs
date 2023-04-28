using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Users.Commands.Register;

public sealed record CreateUserCommand(
	string Firstname,
	string Lastname,
	string Email,
	string Password) : ICommand<Guid>;
