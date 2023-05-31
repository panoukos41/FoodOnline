namespace FoodOnline.Orders.Requests;

public sealed record CreateOrder : CreateCommand<Order>
{
    /// <summary>
    /// A token unique for the order created by the client.
    /// </summary>
    public Guid IdempotencyToken { get; set; }

    public CreateOrder(Order entity) : base(entity)
    {
    }
}
