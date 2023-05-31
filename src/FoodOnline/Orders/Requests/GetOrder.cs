namespace FoodOnline.Orders.Requests;

public sealed record GetOrder : GetQuery<Order>
{
    public GetOrder(Uuid id) : base(id)
    {
    }
}
