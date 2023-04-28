using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Domain.ValueObjects.User;

public sealed partial class Password : ValueObject
{
	public const int MinValue = 8;
	public const int MaxValue = 32;

	[GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,32}$", RegexOptions.IgnoreCase, "en-US")]
	private static partial Regex PasswordMatchGeneratedRegex();

	public string Value { get; private set; }

	public Password(string value)
	{
		Value = value;
	}

	public Password()
	{

	}

	public static Result<Password> Create(string password) =>
		Result.Create(password)
			.Ensure(
				e => !string.IsNullOrWhiteSpace(e),
				DomainErrors.Password.Empty)
			.Ensure(
				e => e.Length > MinValue,
				DomainErrors.Password.TooShort)
			.Ensure(
				e => e.Length < MaxValue,
				DomainErrors.Password.TooLong)
			.Ensure(
				e => PasswordMatchGeneratedRegex().IsMatch(e),
				DomainErrors.Password.Invalid)
			.Map(e => new Password(e));

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}
}
