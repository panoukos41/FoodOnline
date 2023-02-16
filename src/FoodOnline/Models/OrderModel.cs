namespace FoodOnline.Models;

public record OrderModel : IModel
{
    public required string Id { get; init; }

    public byte[] Rev { get; init; } = Array.Empty<byte>();
}
