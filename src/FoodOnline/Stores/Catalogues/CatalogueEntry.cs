using Core.Tools;

namespace FoodOnline.Stores.Catalogues;

public sealed record CatalogueEntry
{
    public required string Id { get; set; } = string.Empty;

    public required string Title { get; set; } = string.Empty;

    public required string Description { get; set; } = string.Empty;

    public decimal DisplayPrice { get; set; }

    public HashSet<CatalogueOption> Options { get; } = new(InlineEqual.For<CatalogueOption>((l, r) => l.Id == r.Id));
}
