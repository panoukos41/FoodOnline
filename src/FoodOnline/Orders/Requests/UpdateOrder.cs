using FoodOnline.Orders.Models;

namespace FoodOnline.Orders.Requests;

public class UpdateOrder : IRequest
{
    public string OrderId { get; set; }

    public string ConnectionId { get; set; }

    public OrderState OrderState { get; set; }
}
