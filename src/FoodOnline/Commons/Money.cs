using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace FoodOnline.Commons;

public sealed record Money :
    IAdditionOperators<Money, Money, Money>,
    ISubtractionOperators<Money, Money, Money>,
    IMultiplyOperators<Money, Money, Money>,
    IDivisionOperators<Money, Money, Money>,
    IEquatable<Money>,
    IEqualityOperators<Money, Money, bool>
{
    public static readonly Money Empty = new(string.Empty, 0);

    public required string Currency { get; init; }

    public required decimal Value { get; init; }

    public Money()
    {
    }

    [SetsRequiredMembers]
    public Money(string currency, decimal value)
    {
        Currency = currency;
        Value = value;
    }

    public static Money operator +(Money left, Money right)
    {
        throw new NotImplementedException();
    }

    public static Money operator -(Money left, Money right)
    {
        throw new NotImplementedException();
    }

    public static Money operator *(Money left, Money right)
    {
        throw new NotImplementedException();
    }

    public static Money operator /(Money left, Money right)
    {
        throw new NotImplementedException();
    }
}
