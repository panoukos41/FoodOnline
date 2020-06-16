using System.Collections.Generic;
using System.Linq;

namespace FoodOnline.Domain.Stores.Catalogue
{
    public class MenuItem
    {
        public MenuItem()
        {
            Entries = new List<MenuItemEntry>();
        }

        public string Title { get; set; }

        public decimal StartPrice { get; set; }

        public virtual decimal PriceEur { get; set; }

        public string Description { get; set; }

        public string ExtraInfo { get; set; }

        public List<MenuItemEntry> Entries { get; set; }

        /// <summary>
        /// Returns a string containing the selected item names seperated by comas.
        /// </summary>
        /// <returns></returns>
        public string SelectedValues()
        {
            var values = new List<string>();

            foreach (var entry in Entries)
                switch (entry)
                {
                    case SelectSingle single:
                        values.Add(single.SelectedName());
                        continue;
                    case SelectMany many:
                        values.AddRange(many.SelectedNames());
                        continue;
                }
            return string.Join(", ", values);
        }

        /// <summary>
        /// This will calculate the price from the start everytime it's called.
        /// </summary>
        public decimal CalculatePrice()
        {
            PriceEur = 0;
            foreach (var entry in Entries)
            {
                PriceEur += entry.CalculatePrice();
            }
            return PriceEur;
        }

        public bool AnyNull()
        {
            return Title is null
                || Entries.Any(x => x.AnyNull());
        }
    }
}