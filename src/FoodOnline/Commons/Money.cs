using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Commons;

public readonly record struct Money
{
    public static readonly Money Empty = new(Currency.EUR, 0);

    public required Currency Currency { get; init; }

    public required decimal Value { get; init; }

    public Money()
    {
    }

    [SetsRequiredMembers]
    public Money(Currency currency, decimal value)
    {
        Currency = currency;
        Value = value;
    }
}

public enum Currency
{
    EUR
}
