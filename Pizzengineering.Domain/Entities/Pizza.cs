using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.DomainEvents;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Domain.ValueObjects.Pizza;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Domain.Entities;

public sealed class Pizza : AggregateRoot, IAuditableEntity
{
	public Pizza(
		Guid id,
		Name name,
		double price,
		bool hasDiscount,
		Rate rate,
		Description description) : base(id)
	{
		Name = name;
		Price = price;
		HasDiscount = hasDiscount;
		Rate = rate;
		Description = description;
	}

	public Name Name { get; private set; }
	public double Price { get; private set; }
	public bool HasDiscount { get; private set; }
	public Rate Rate { get; private set; }
	public Description Description { get; private set; }
	public DateTime CreatedOnUtc { get; init; }
	public DateTime? LastModifiedUtc { get; set; }

	public static Pizza Create(
		Guid id,
		Name name,
		Description description,
		Rate rate,
		double price,
		bool hasDiscount)
	{
		var pizza = new Pizza(
			id,
			name,
			price,
			hasDiscount,
			rate,
			description)
		{
			CreatedOnUtc = DateTime.UtcNow,
			LastModifiedUtc = DateTime.UtcNow
		};

		return pizza;
	}

	public void ChangeVariableInformation(
		Rate rate, 
		double price, 
		bool hasDiscount)
	{
		if (!Rate.Value.Equals(rate.Value) ||
			!Price.Equals(price) ||
			!HasDiscount.Equals(hasDiscount))
		{
			RaiseDomainEvent(
				new PizzaUpdatedDomainEvent(Guid.NewGuid(), Id));
		}

		Rate = rate;
		Price = price;
		HasDiscount = hasDiscount;
		LastModifiedUtc = DateTime.UtcNow;
	}
}
