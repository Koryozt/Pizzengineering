using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Domain.ValueObjects.Pizza;

public sealed class Rate : ValueObject
{
	public double Value { get; set; }

	private Rate(double value)
	{
		Value = value;
	}

	private Rate()
	{

	}

	public static Result<Rate> Create(double value) =>
		Result.Create(value)
			.Ensure(
				e => e > 0.0,
				DomainErrors.Rate.Invalid)
			.Ensure(
				e => e < 5.0,
				DomainErrors.Rate.Invalid)
			.Map(e => new Rate(e));

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}
}
