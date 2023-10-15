namespace FoodOnline.Stores.Requests;

public sealed record UpdateStore : Command<Store, Void>
{
    public UpdateStore(Store entity) : base(entity)
    {
    }
}
