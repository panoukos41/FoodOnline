using Core.Tools;

namespace FoodOnline.Stores.Catalogues;

public sealed record Catalogue
{
    public required string Title { get; set; } = string.Empty;

    public required Currency Currency { get; set; } = Currency.EUR;

    public HashSet<CatalogueEntry> Entries { get; } = new(InlineEqual.For<CatalogueEntry>((l, r) => l.Id == r.Id));
}
