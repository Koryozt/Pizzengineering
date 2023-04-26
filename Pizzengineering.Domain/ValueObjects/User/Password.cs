using System.Text.RegularExpressions;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Domain.ValueObjects.User;

public sealed class Password : ValueObject
{
	public const int MinValue = 8;
	public const int MaxValue = 32;
	public const string Pattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]";

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
				e => string.IsNullOrEmpty(e),
				DomainErrors.Password.Empty)
			.Ensure(
				e => e.Length > MinValue,
				DomainErrors.Password.TooShort)
			.Ensure(
				e => e.Length < MaxValue,
				DomainErrors.Password.TooLong)
			.Ensure(
				e => Regex.IsMatch(e, Pattern),
				DomainErrors.Password.Invalid)
			.Map(e => new Password(e));

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}
}
