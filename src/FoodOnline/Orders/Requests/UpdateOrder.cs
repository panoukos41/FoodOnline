namespace FoodOnline.Orders.Requests;

public class UpdateOrder : IRequest
{
    public Uuid OrderId { get; set; }

    public Uuid ConnectionId { get; set; }

    public OrderState OrderState { get; set; }
}
