namespace FoodOnline.Stores.Requests;

public sealed record GetStore : FindQuery<Store>
{
    public GetStore(Uuid id) : base(id)
    {
    }
}
