namespace FoodOnline.Orders.Requests;

public sealed record PlaceOrder : Command<OrderModel, Uuid>
{
    /// <summary>
    /// A token unique for the order created by the client.
    /// </summary>
    public Guid IdempotencyToken { get; set; }

    public PlaceOrder(OrderModel entity) : base(entity)
    {
    }
}
