using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;

namespace Pizzengineering.Application.Users.Queries.GetUserById;
public sealed record GetUserByIdQuery(Guid Id) : IQuery<UserResponse>;
