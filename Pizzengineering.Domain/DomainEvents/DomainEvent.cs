using Pizzengineering.Domain.Primitives;

namespace Pizzengineering.Domain.DomainEvents;

public abstract record DomainEvent(Guid Id) : IDomainEvent;