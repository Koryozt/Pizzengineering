using Pizzengineering.Domain.DomainEvents;
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

	public Guid UserId { get; private set; }

	public User User { get; private set; }
	public ICollection<Pizza> Pizzas { get; private set; }
	public double TotalPrice => Pizzas.Sum(x => x.Price);
	public DateTime PurchasedAtUtc { get; private set; }


	public DateTime CreatedOnUtc { get; init; }
	public DateTime? LastModifiedUtc { get; set; }

	public static Order Create(
		Guid id,
		User user,
		ICollection<Pizza> pizzas,
		DateTime purchasedAtUtc)
	{
		if (user is null || pizzas.Count is 0)
		{
			return default!;
		}

		var order = new Order(
			id,
			user,
			pizzas,
			purchasedAtUtc)
		{
			UserId = user.Id,
			CreatedOnUtc = DateTime.UtcNow,
			LastModifiedUtc = DateTime.UtcNow
		};

		order.RaiseDomainEvent(
			new OrderCreatedDomainEvent(Guid.NewGuid(), id));

		return order;
	}
}
