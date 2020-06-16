using FoodOnline.Domain.ValueObjects;

namespace FoodOnline.Domain.Stores.Models
{
    public class StoreBusinessModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Catalogue { get; set; }

        public bool IsOpen { get; set; }

        public bool IsPublished { get; set; }

        public Address Address { get; set; }
    }
}