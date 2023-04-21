using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Domain.ValueObjects.PaymentInformation;

public sealed class CreditCardNumber : ValueObject
{
	public string Value { get; private set; }
	public const string Pattern = @"^\d+$";

	private CreditCardNumber(string value)
	{
		Value = value;
	}

	private CreditCardNumber()
	{

	}

	public static Result<CreditCardNumber> Create(string value) =>
		Result.Create(value)
			.Ensure(
				e => !string.IsNullOrEmpty(e),
				DomainErrors.CreditCardNumber.Empty)
			.Ensure(
				e => Regex.IsMatch(e, Pattern),
				DomainErrors.CreditCardNumber.NotOnlyDigits)
			.Ensure(
				e => IsCardNumberValid(e),
				DomainErrors.CreditCardNumber.Invalid)
			.Map(e => new CreditCardNumber(e));

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}

	private static bool IsCardNumberValid(string value)
	{
		int i, checkSum = 0;

		for (i = value.Length - 1; i >= 0; i -= 2)
			checkSum += (value[i] - '0');

		for (i = value.Length - 2; i >= 0; i -= 2)
		{
			int val = ((value[i] - '0') * 2);
			while (val > 0)
			{
				checkSum += (val % 10);
				val /= 10;
			}
		}

		return ((checkSum % 10) == 0);
	}
}
