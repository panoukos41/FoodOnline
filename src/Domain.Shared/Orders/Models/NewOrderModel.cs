using FoodOnline.Domain.ValueObjects;

namespace FoodOnline.Domain.Orders.Models
{
    public class NewOrderModel
    {
        /// <summary>
        /// The id of the order.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// The connection Id to respond to.
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// A yaml list of the order entries of type OrderItems.
        /// </summary>
        public string Entries { get; set; }

        /// <summary>
        /// The total price of the order in EUR.
        /// </summary>
        public decimal TotalPriceEur { get; set; }

        /// <summary>
        /// The address to which it should be delivered.
        /// This is a <see cref="ValueObjectBase"/>
        /// </summary>
        public Address Address { get; set; }
    }
}