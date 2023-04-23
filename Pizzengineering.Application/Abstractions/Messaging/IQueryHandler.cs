using Pizzengineering.Domain.Shared;
using MediatR;

namespace Pizzengineering.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}