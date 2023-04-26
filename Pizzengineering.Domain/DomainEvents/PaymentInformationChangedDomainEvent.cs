namespace Pizzengineering.Domain.DomainEvents;

public sealed record PaymentInformationChangedDomainEvent(Guid Id, Guid PaymentId) : DomainEvent(Id);
