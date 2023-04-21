using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Domain.Entities;

public sealed class User : AggregateRoot, IAuditableEntity
{
	private readonly List<Order> _ordersMade = new();

	public User(
		Guid id,
		Name firstName,
		Name lastName,
		Email email,
		Password password,
		PaymentInformation paymentInformation) : base(id) 
	{
		Firstname = firstName;
		Lastname = lastName;
		Email = email;
		Password = password;
		PaymentInformation = paymentInformation;
	}

	public Name Firstname { get; private set; }
	public Name Lastname { get; private set; }
	public Email Email { get; private set; }
	public Password Password { get; private set; }
	public PaymentInformation PaymentInformation { get; private set; }
	public ICollection<Role> Role { get; set; }
	public IReadOnlyCollection<Order> OrdersMade => _ordersMade;

	public DateTime CreatedOnUtc { get; init; }
	public DateTime? LastModifiedUtc { get; set; }
}
