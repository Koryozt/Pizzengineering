namespace Pizzengineering.Domain.DomainEvents;

public sealed record PizzaUpdatedDomainEvent(Guid Id, Guid PizzaId) : DomainEvent(Id);