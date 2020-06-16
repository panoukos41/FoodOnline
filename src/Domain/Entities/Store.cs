using FoodOnline.Domain.ValueObjects;
using System.Collections.Generic;

namespace FoodOnline.Domain.Entities
{
    public class Store
    {
        public Store()
        {
            Employees = new List<StoreUser>();
            Orders = new List<Order>();
        }

        /// <summary>
        /// The primary key of the entity.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// If true the store is published.
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// If true the store is open.
        /// </summary>
        public bool Open { get; set; }

        /// <summary>
        /// The name of the store.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Description of the store.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A json list of Catalogue items.
        /// </summary>
        public string Catalogue { get; set; }

        /// <summary>
        /// The address of the store.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// A string of regions to deliver to seperated by coma ","
        /// </summary>
        public string DeliversTo { get; set; }

        /// <summary>
        /// The id of the owner.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// The owner of the store.
        /// </summary>
        public StoreUser Owner { get; set; }

        /// <summary>
        /// The employes that have access to the store.
        /// </summary>
        public ICollection<StoreUser> Employees { get; private set; }

        /// <summary>
        /// The orders of a store.
        /// </summary>
        public ICollection<Order> Orders { get; private set; }
    }
}