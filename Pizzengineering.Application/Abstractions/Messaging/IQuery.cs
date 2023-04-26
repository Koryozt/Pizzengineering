using MediatR;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}