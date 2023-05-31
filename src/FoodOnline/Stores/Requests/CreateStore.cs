namespace FoodOnline.Stores.Requests;

public sealed record CreateStore : CreateCommand<Store>
{
    public required Uuid Owner { get; init; }

    public CreateStore(Store entity) : base(entity)
    {
    }
}
