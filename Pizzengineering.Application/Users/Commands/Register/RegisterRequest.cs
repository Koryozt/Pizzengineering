﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Users.Commands.Register;

public sealed record RegisterRequest(
	Name Firstname,
	Name Lastname,
	Email Email,
	Password Password);