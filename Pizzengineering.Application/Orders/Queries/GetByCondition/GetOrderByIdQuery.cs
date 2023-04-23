using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;

namespace Pizzengineering.Application.Orders.Queries.GetByCondition;

public sealed record GetOrderByIdQuery(Guid Id) : IQuery<OrderResponse>;
