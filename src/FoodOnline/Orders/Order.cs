namespace FoodOnline.Orders;

public static class Order
{
    public sealed record Place : Command<OrderModel, CreatedResponse>
    {
        public Place(OrderModel data) : base(data)
        {
        }
    }

    public sealed record Find : FindQuery<OrderModel>
    {
        public Find(Uuid id) : base(id)
        {
        }
    }

    public record ChangeState : Command<Void>
    {
        public required Uuid OrderId { get; init; }

        public required OrderState OrderState { get; init; }
    }
}
