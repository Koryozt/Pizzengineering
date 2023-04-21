using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Primitives;

namespace Pizzengineering.Domain.Entities;

public sealed class Order : AggregateRoot, IAuditableEntity
{
	public Order(
		Guid id,
		User user,
		ICollection<Pizza> pizzas,
		DateTime purchasedAtUtc) : base(id)
	{
		User = user;
		Pizzas = pizzas;
		PurchasedAtUtc = purchasedAtUtc;
	}

	public 	Guid UserGuid { get; private set; }

	public User User { get; private set; }
	public ICollection<Pizza> Pizzas { get; private set; }
	public double TotalPrice => Pizzas.Sum(x => x.Price);
	public DateTime PurchasedAtUtc { get; private set; }


	public DateTime CreatedOnUtc { get; init; }
	public DateTime? LastModifiedUtc { get; set; }
}
