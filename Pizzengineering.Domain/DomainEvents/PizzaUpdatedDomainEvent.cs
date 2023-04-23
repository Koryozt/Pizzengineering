﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzengineering.Domain.DomainEvents;

public sealed record PizzaUpdatedDomainEvent(Guid Id, Guid PizzaId) : DomainEvent(Id);