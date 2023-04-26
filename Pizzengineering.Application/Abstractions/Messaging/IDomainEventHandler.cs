using MediatR;
using Pizzengineering.Domain.Primitives;

namespace Pizzengineering.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
	where TEvent : IDomainEvent
{
}
