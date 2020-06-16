namespace FoodOnline.Domain.Stores.Catalogue
{
    public class MenuItemEntry
    {
        public string Name { get; set; }

        public virtual decimal PriceEur { get; set; }

        /// <summary>
        /// This will return the PriceEur it was added for
        /// easier compatibility with derived classes.
        /// </summary>
        public virtual decimal CalculatePrice() => PriceEur;

        public virtual bool AnyNull()
        {
            return Name == null;
        }
    }
}