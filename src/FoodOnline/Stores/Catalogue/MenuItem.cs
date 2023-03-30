namespace FoodOnline.Stores.Catalogue;

public abstract class MenuItem
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<MenuItemEntry> Entries { get; init; } = new();

    public bool IsSingle() => Entries.Count == 1;
}
