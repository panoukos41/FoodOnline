namespace FoodOnline.Orders.Requests;

public sealed record SetOrderState : Command<Void>
{
    public Uuid OrderId { get; }

    public OrderState State { get; }

    public SetOrderState(Uuid orderId, OrderState state)
    {
        OrderId = orderId;
        State = state;
    }
}
