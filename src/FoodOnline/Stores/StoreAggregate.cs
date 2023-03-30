namespace FoodOnline.Stores;

public sealed record StoreAggregate
{
    public required Uuid Owner { get; init; }

    public required Store Store { get; init; }
}
