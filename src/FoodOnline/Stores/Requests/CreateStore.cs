namespace FoodOnline.Stores.Requests;

public sealed record CreateStore : Command<Store, CreatedResponse>
{
    public required Uuid Owner { get; init; }

    public CreateStore(Store entity) : base(entity)
    {
    }
}
