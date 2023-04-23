using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzengineering.Domain.DomainEvents;

public sealed record PaymentInformationChangedDomainEvent(Guid Id, Guid PaymentId) : DomainEvent(Id);
