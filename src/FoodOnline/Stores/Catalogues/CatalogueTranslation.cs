namespace FoodOnline.Stores.Catalogues;

public sealed record CatalogueTranslation
{
    public required string Id { get; set; } = string.Empty;

    public required string Title { get; set; } = string.Empty;

    public required string Description { get; set; } = string.Empty;
}
