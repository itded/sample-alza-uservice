using CSharpFunctionalExtensions;

namespace Alza.UService.Domain.Common;

/// <summary>
/// Non-negative price
/// </summary>
public class Price : ValueObject<Price>
{
    public static Result<Price> Create(decimal value)
    {
        if (value < 0m)
        {
            return Result.Failure<Price>($"The value cannot be negative: {value}");
        }

        return new Price(value);
    }

    private Price(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }


    protected override bool EqualsCore(Price other)
    {
        return Value == other.Value;
    }

    protected override int GetHashCodeCore()
    {
        return (Value.GetHashCode() * 397) ^ Value.GetHashCode();
    }
}
