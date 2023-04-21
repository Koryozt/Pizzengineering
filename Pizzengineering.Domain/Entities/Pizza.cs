using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
