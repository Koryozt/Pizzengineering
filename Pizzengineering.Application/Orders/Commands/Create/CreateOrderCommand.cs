using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Domain.Entities;

namespace Pizzengineering.Application.Orders.Commands.Create;

public sealed record CreateOrderCommand(
	ICollection<Guid> PizzaIds,
	DateTime PurchasedAtUtc) : ICommand<Guid>;
