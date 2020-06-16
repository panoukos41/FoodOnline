using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOnline.Domain.Stores.Catalogue
{
    public class SelectSingle : MenuItemEntry
    {
        public SelectSingle()
        {
            Items = new List<MenuItemEntry>();
        }

        public List<MenuItemEntry> Items { get; set; }

        public int Selected { get; set; }

        /// <summary>
        /// Retruns the selected item's name value.
        /// </summary>
        /// <returns></returns>
        public string SelectedName() => Items[Selected].Name;

        /// <summary>
        /// This will calculate the price from the start everytime it's called.
        /// </summary>
        public override decimal CalculatePrice()
        {
            return PriceEur = Items[Selected].CalculatePrice();
        }

        public override bool AnyNull()
        {
            return Name == null
                || Items.Any(x => x.AnyNull());
        }
    }
}