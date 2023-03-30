using FluentValidation;

namespace FoodOnline.Stores.Requests;

public sealed record QueryStores : Query<IEnumerable<Store>>, IValid
{
    public Location Location { get; init; } = Location.Empty;

    public static IValidator Validator { get; } = new InlineValidator<QueryStores>
    {

    };
}
