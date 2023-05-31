namespace FoodOnline.Orders.Requests;

public sealed record SetOrderState : PatchCommand<Order>
{
    public OrderState State { get; }

    public SetOrderState(Uuid id, OrderState state) : base(id)
    {
        State = state;
    }
}
