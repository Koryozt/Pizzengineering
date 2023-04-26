using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Domain.ValueObjects.Pizza;

public sealed class Description : ValueObject
{
	public string Value { get; set; }
	public const int MaxValue = 500;

	private Description(string value)
	{
		Value = value;
	}

	private Description()
	{

	}

	public static Result<Description> Create(string value) =>
		Result.Create(value)
			.Ensure(
				e => !string.IsNullOrEmpty(e),
				DomainErrors.Description.Empty)
			.Ensure(
				e => e.Length < MaxValue,
				DomainErrors.Description.TooLong)
			.Map(e => new Description(e));

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}
}
