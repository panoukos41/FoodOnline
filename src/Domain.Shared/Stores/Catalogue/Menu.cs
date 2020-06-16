using System.Collections.Generic;
using System.Linq;

namespace FoodOnline.Domain.Stores.Catalogue
{
    public class Menu
    {
        public Menu()
        {
            Version = 1;
            Categories = new List<MenuCategory>();
        }

        public int Version { get; set; }

        public List<MenuCategory> Categories { get; set; }

        public bool AnyNull()
        {
            return Categories.Any(x => x.AnyNull());
        }
    }
}