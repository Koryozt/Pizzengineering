using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Pizzengineering.Domain.Primitives;

public interface IDomainEvent : INotification
{
}
