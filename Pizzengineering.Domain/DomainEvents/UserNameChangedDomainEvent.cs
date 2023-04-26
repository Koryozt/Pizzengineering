﻿namespace Pizzengineering.Domain.DomainEvents;

public sealed record UserNameChangedDomainEvent(Guid Id, Guid UserId) : DomainEvent(Id);
