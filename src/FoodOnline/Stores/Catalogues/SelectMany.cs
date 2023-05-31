//namespace FoodOnline.Stores.Catalogue;

//public class SelectMany// : MenuItemEntry
//{
//    public SelectMany()
//    {
//        Items = new List<MenuItemEntry>();
//        Selected = new List<int>();
//    }

//    public List<MenuItemEntry> Items { get; set; }

//    public List<int> Selected { get; set; }

//    /// <summary>
//    /// Returns an IEnumerable of strings that contains name value
//    /// of the selected items.
//    /// </summary>
//    /// <returns></returns>
//    public IEnumerable<string> SelectedNames() => Items
//        .Where((item, index) => Selected.Contains(index))
//        .Select(item => item.Name);

//    /// <summary>
//    /// This will calculate the price from the start everytime it's called.
//    /// </summary>
//    public override decimal CalculatePrice()
//    {
//        PriceEur = 0;
//        foreach (var selection in Selected)
//        {
//            PriceEur += Items[selection].CalculatePrice();
//        }
//        return PriceEur;
//    }

//    public override bool AnyNull()
//    {
//        return Name == null
//            || Items.Any(x => x.AnyNull());
//    }
//}