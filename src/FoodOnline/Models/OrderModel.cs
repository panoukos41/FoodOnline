namespace FoodOnline.Models;

public record OrderModel : Valid<StoreModel, StoreModelValidator>, IModel
{
    public required Uuid Id { get; init; }
}
