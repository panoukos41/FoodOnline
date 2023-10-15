namespace FoodOnline.Orders.Requests;

public sealed record UpdateOrder : Command<Void>
{
    public Uuid OrderId { get; set; }

    public Uuid ConnectionId { get; set; }

    public OrderState OrderState { get; set; }
}
