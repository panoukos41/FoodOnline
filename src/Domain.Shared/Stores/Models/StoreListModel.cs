using FoodOnline.Domain.ValueObjects;

namespace FoodOnline.Domain.Stores.Models
{
    public class StoreListModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsOpen { get; set; }

        public Address Address { get; set; }
    }
}