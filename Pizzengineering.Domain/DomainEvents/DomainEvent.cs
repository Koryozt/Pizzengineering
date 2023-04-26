using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Primitives;

namespace Pizzengineering.Domain.DomainEvents;

public abstract record DomainEvent(Guid Id) : IDomainEvent;