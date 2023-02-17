using FluentValidation;

namespace FoodOnline.Models;

public record StoreModel : Valid<StoreModel, StoreModelValidator>, IModel
{
    public required Uuid Id { get; init; }
}

public sealed class StoreModelValidator : AbstractValidator<StoreModel>
{
    public StoreModelValidator()
    {
    }
}
