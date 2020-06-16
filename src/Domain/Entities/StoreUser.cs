using FoodOnline.Domain.Common;
using System.Collections.Generic;

namespace FoodOnline.Domain.Entities
{
    public class StoreUser : LoginEntity
    {
        /// <summary>
        /// The user Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The role that can ge <see cref="Role.StoreOwner"/> or <see cref="Role.StoreEmployee"/>
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// The id the user belongs to if the role is a <see cref="Role.StoreEmployee"/>
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// The Store object for the StoreId
        /// </summary>
        public Store Store { get; set; }

        /// <summary>
        /// The stores the user has if the role is <see cref="Role.StoreOwner"/>
        /// </summary>
        public IList<Store> Stores { get; private set; }
    }
}