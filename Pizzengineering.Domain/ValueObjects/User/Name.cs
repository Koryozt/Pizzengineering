using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Domain.ValueObjects.User;

public sealed class Name : ValueObject
{
    public const int MaxLength = 50;
    public string Value { get; private set; }

    private Name(string value)
    {
        Value = value;
    }

    private Name()
    {

    }

    public static Result<Name> Create(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure<Name>
                (DomainErrors.FirstName.Empty);
        }

        if (firstName.Length > MaxLength)
        {
            return Result.Failure<Name>
                (DomainErrors.FirstName.TooLong);
        }

        var firstname = new Name(firstName);

        return Result.Success(firstname);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
