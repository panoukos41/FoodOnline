namespace FoodOnline.Stores.Requests;

public sealed record UpdateStore : UpdateCommand<Store>
{
    public UpdateStore(Store entity) : base(entity)
    {
    }
}
