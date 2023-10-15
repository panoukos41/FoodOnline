namespace FoodOnline.Orders.Requests;

public sealed record FindOrder : FindQuery<OrderModel>
{
    public FindOrder(Uuid id) : base(id)
    {
    }
}
