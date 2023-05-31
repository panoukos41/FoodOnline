namespace FoodOnline.Stores.Requests;

public sealed record GetStore : GetQuery<Store>
{
    public GetStore(Uuid id) : base(id)
    {
    }
}
