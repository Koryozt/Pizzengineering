using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.DomainEvents;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Domain.Entities;

public sealed class User : AggregateRoot, IAuditableEntity
{
	private readonly List<Order> _ordersMade = new();
	private const string Pepper = "LqBRuuWfrnTsmrjNdi";
	private const string SpecialChar = "-";

	public User(
		Guid id,
		Name firstName,
		Name lastName,
		Email email,
		Password password) : base(id) 
	{
		Firstname = firstName;
		Lastname = lastName;
		Email = email;
		Password = password;
	}

	private User()
	{

	}

	public Name Firstname { get; private set; }
	public Name Lastname { get; private set; }
	public Email Email { get; private set; }
	public Password Password { get; private set; }
	public PaymentInformation PaymentInformation { get; private set; }
	public string Salt { get; init; }
	public ICollection<Role> Roles { get; set; }
	public IReadOnlyCollection<Order> OrdersMade => _ordersMade;

	public DateTime CreatedOnUtc { get; init; }
	public DateTime? LastModifiedUtc { get; set; }

	public static User Create(
		Guid id,
		Name firstName,
		Name lastName,
		Email email,
		Password password)
	{
		string salt = GenerateSalt();
		
		Password hashed = ComputeHash(password, salt);

		var user = new User(
			id,
			firstName,
			lastName,
			email,
			hashed)
		{
			Roles = new Role[] { Role.Registered },
			Salt = salt,
			CreatedOnUtc = DateTime.UtcNow,
			LastModifiedUtc = DateTime.UtcNow
		};

		user.RaiseDomainEvent(new UserRegisteredDomainEvent(
				Guid.NewGuid(),
				id));

		return user;
	}

	private static Password ComputeHash(Password password, string salt)
	{
		using var sha256 = SHA256.Create();
		var mixed = password.Value + salt + Pepper;
		
		byte[] byteValue = Encoding.UTF8.GetBytes(mixed),
				byteHash = sha256.ComputeHash(byteValue);

		int index = 5;

		string hash = Convert
			.ToBase64String(byteHash)
			.Substring(0, 30)
			.Insert(index, SpecialChar);

		var result = Password.Create(hash);

		try
		{
			return result.Value;
		}
		catch (Exception)
		{
			throw;
		}
	}

	private static string GenerateSalt()
	{
		using var rng = RandomNumberGenerator.Create();
		var byteSalt = new byte[16];

		rng.GetBytes(byteSalt);

		string salt = Convert.ToBase64String(byteSalt);

		return salt;
	}

	public void ChangeNames(Name firstName, Name lastName)
	{
		if (!Firstname.Value.Equals(firstName.Value) || !Lastname.Value.Equals(lastName.Value))
		{
			RaiseDomainEvent(new UserNameChangedDomainEvent(
				Guid.NewGuid(), Id));
		}

		Firstname = firstName;
		Lastname = lastName;
	}
}
