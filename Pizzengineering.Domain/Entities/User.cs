using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Domain.Entities;

public sealed class User : AggregateRoot, IAuditableEntity
{
	private readonly List<Order> _ordersMade = new();
	private static int Iterations = 5;
	private const string Pepper = "LqBRuuWfrnTsmrjNdi";

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
	public string Salt { get; init; }
	public ICollection<Role> Role { get; set; }
	public IReadOnlyCollection<Order> OrdersMade => _ordersMade;

	public DateTime CreatedOnUtc { get; init; }
	public DateTime? LastModifiedUtc { get; set; }

	public static User Create(
		Guid id,
		Name firstName,
		Name lastName,
		Email email,
		Password password,
		PaymentInformation paymentInformation)
	{
		string salt = GenerateSalt();
		string hashed = ComputeHash(password, salt);

		var result = Password.Create(hashed);

		if (result.IsFailure)
		{
			return default!;
		}

		var passwordHashed = result.Value;

		var user = new User(
			id,
			firstName,
			lastName,
			email,
			passwordHashed,
			paymentInformation)
		{
			Salt = salt,
			CreatedOnUtc = DateTime.UtcNow,
			LastModifiedUtc = DateTime.UtcNow
		};

		return user;
	}

	private static string ComputeHash(Password password, string salt)
	{
		string pass = password.Value;

		if (Iterations <= 0)
			return pass;

		using var sha256 = SHA256.Create();
		var mixed = password + salt + Pepper;
		
		byte[] byteValue = Encoding.UTF8.GetBytes(mixed),
				byteHash = sha256.ComputeHash(byteValue);
		
		string hash = Convert.ToBase64String(byteHash);

		var result = Password.Create(hash);

		if (result.IsSuccess)
		{
			--Iterations;

			return ComputeHash(result.Value, salt);
		}

		return pass;
	}

	private static string GenerateSalt()
	{
		using var rng = RandomNumberGenerator.Create();
		var byteSalt = new byte[16];

		rng.GetBytes(byteSalt);

		string salt = Convert.ToBase64String(byteSalt);

		return salt;
	}
}
