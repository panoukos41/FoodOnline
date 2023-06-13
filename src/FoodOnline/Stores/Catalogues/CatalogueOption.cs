using Dunet;
using FoodOnline.Tools;
using System.Text.Json.Serialization;

namespace FoodOnline.Stores.Catalogues;

[JsonDerivedType(typeof(SingleChoice), "single-choice")]
[JsonDerivedType(typeof(MultiChoice), "multi-choice")]
[Union, JsonPolymorphic]
public partial record CatalogueOption
{
    public required string Id { get; set; } = string.Empty;

    public required string Title { get; set; } = string.Empty;

    public required string Description { get; set; } = string.Empty;

    public partial record SingleChoice
    {
        public HashSet<CatalogueChoice> Choices { get; } = new(FlEqual.For<CatalogueChoice>((l, r) => l.Id == r.Id));
    }

    public partial record MultiChoice
    {
        public HashSet<CatalogueChoice> Choices { get; } = new(FlEqual.For<CatalogueChoice>((l, r) => l.Id == r.Id));
    }
}

public sealed record CatalogueChoice
{
    public required string Id { get; set; } = string.Empty;

    public required string Title { get; set; } = string.Empty;

    public required string Description { get; set; } = string.Empty;

    public required decimal Price { get; set; }
}
