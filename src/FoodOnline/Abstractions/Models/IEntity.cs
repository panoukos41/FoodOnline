namespace FoodOnline.Abstractions.Models;

public interface IId
{
    Uuid Id { get; init; }
}

public interface IEntity : IId, IValid
{
    //Uuid Id { get; init; }
}
