using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Domain.ValueObjects.Pizza;

public sealed class Rate : ValueObject
{
	public double Value { get; set; }
	public const double MinValue = 0.0;
	public const double MaxValue = 5.0;

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
				e => e > MinValue,
				DomainErrors.Rate.Invalid)
			.Ensure(
				e => e < MaxValue,
				DomainErrors.Rate.Invalid)
			.Map(e => new Rate(e));

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}
}
