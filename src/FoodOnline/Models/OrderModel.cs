namespace FoodOnline.Models;

public record OrderModel : IModel
{
    public required Uuid Id { get; init; }
}
