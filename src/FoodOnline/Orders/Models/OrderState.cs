namespace FoodOnline.Orders.Models;

public enum OrderState
{
    Sending,
    Received,
    Confirmed,
    Delivering,
    Delivered,
    Canceled
}
