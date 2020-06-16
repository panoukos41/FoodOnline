using FoodOnline.Domain.Enums;
using FoodOnline.Domain.Stores.Models;
using FoodOnline.Domain.ValueObjects;

namespace FoodOnline.Domain.Orders.Models
{
    public class OrderModel
    {
        public OrderState State { get; set; }

        public Address Address { get; set; }

        public StoreListModel Store { get; set; }

        public decimal TotalPriceEur { get; set; }

        public string Entries { get; set; }
    }
}