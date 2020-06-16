using FoodOnline.Domain.Common;
using FoodOnline.Domain.Enums;
using FoodOnline.Domain.ValueObjects;
using System;

namespace FoodOnline.Domain.Entities
{
    public class Order : AuditableEntity
    {
        /// <summary>
        /// The primary key of the entity.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A token unique for the order created by the client.
        /// </summary>
        public Guid IdempotencyToken { get; set; }

        /// <summary>
        /// A json list of the order entries of type OrderItems.
        /// </summary>
        public string Entries { get; set; }

        /// <summary>
        /// The total price of the order in EUR.
        /// </summary>
        public decimal TotalPriceEur { get; set; }

        /// <summary>
        /// The state the order is in.
        /// </summary>
        public OrderState State { get; set; }

        /// <summary>
        /// The address to which it should be delivered.
        /// This is a <see cref="ValueObjectBase"/>
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// The id of the store.
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// Navigation property. The store this order was placed for.
        /// </summary>
        public Store Store { get; set; }

        /// <summary>
        /// The id of the user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation property. The user that placed this order.
        /// </summary>
        public User User { get; set; }
    }
}