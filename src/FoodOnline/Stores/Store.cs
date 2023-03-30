using FluentValidation;

namespace FoodOnline.Stores;

public sealed record Store : IEntity
{
    public required Uuid Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public Address Address { get; init; } = Address.Empty;

    public Location Location { get; init; } = Location.Empty;

    public bool Open { get; set; }

    public bool Published { get; set; }

    public static IValidator Validator { get; } = new StoreValidator();
}

public sealed class StoreValidator : AbstractValidator<Store>
{
    public StoreValidator()
    {
    }
}

//{
//    public string Id { get; set; }

//public string Name { get; set; }

//public string Description { get; set; }

//public string Catalogue { get; set; }

//public bool IsOpen { get; set; }

//public bool IsPublished { get; set; }

//public Address Address { get; set; }
//}

//public class StoreListModel
//{
//    public string Id { get; set; }

//    public string Name { get; set; }

//    public bool IsOpen { get; set; }

//    public Address Address { get; set; }
//}
