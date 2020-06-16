using FoodOnline.Domain.Enums;
using FoodOnline.Domain.Stores.Catalogue;
using FoodOnline.Domain.ValueObjects;
using System.Collections.Generic;

namespace FoodOnline.WebClient.Business.Models
{
    public class Order
    {
        public Order()
        {
            Entries = new List<MenuItem>();
        }

        /// <summary>
        /// The id of the order.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// The connection Id to respond to.
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// A list of entries.
        /// </summary>
        public List<MenuItem> Entries { get; set; }

        /// <summary>
        /// The total price of the order in EUR.
        /// </summary>
        public decimal TotalPriceEur { get; set; }

        /// <summary>
        /// The order's state.
        /// </summary>
        public OrderState State { get; set; }

        /// <summary>
        /// The address to which it should be delivered.
        /// This is a <see cref="ValueObjectBase"/>
        /// </summary>
        public string Address { get; set; }
    }
}