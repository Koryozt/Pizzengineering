using Pizzengineering.Domain.Primitives;
using MediatR;

namespace Pizzengineering.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
