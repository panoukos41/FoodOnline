namespace FoodOnline.Stores.Catalogue;

public sealed record Menu
{
    public Menu()
    {
        Version = 1;
    }

    public int Version { get; }

    public HashSet<MenuItem> Items { get; init; } = new();
}
