using Pizzengineering.Application.Abstractions.Messaging;

namespace Pizzengineering.Application.Users.Queries.GetUserById;
public sealed record GetUserByIdQuery(Guid Id) : IQuery<UserResponse>;
