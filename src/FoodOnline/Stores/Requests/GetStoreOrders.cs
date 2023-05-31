using FoodOnline.Orders;


namespace FoodOnline.Stores.Requests;

public sealed record GetStoreOrders : GetQuery<IEnumerable<Order>>
{
    public GetStoreOrders(Uuid id) : base(id)
    {
    }
}
