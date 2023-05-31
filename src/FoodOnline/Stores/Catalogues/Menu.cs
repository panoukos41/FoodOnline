using Dunet;

namespace FoodOnline.Stores.Catalogues;

public sealed record Menu
{
    public Currency Currency { get; set; }

    public HashSet<MenuItem> Items { get; init; } = new();
}

public partial record MenuItem
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal StartingPrice { get; set; }

    public HashSet<MenuItemEntry> Entries { get; init; } = new();
}

[Union]
public partial record MenuItemEntry
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Dictionary<string, decimal> Options { get; init; } = new();

    public partial record Single
    {

    }

    public partial record Multi
    {

    }
}
