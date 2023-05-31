namespace FoodOnline.Abstractions;

public interface IEntity : IValid
{
    Uuid Id { get; init; }
}
