using System.Collections.Generic;
using System.Linq;

namespace FoodOnline.Domain.Stores.Catalogue
{
    public class MenuCategory
    {
        public MenuCategory()
        {
            MenuItems = new List<MenuItem>();
        }

        public string Title { get; set; }

        public List<MenuItem> MenuItems { get; set; }

        public bool AnyNull()
        {
            return Title == null
                || MenuItems.Any(x => x.AnyNull());
        }
    }
}