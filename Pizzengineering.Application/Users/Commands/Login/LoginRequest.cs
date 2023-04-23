using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Users.Commands.Login;

public sealed record LoginRequest(
	Email Email,
	Password Password);
