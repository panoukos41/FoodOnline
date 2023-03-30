namespace FoodOnline.Stores.Requests;

public sealed record GetStore : GetRequest<Store>
{
    public GetStore(Uuid id) : base(id)
    {
    }
}
