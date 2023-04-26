using Pizzengineering.Domain.Shared;
using MediatR;

namespace Pizzengineering.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}