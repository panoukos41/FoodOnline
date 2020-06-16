using System.Collections.Generic;

namespace FoodOnline.Domain.Entities
{
    public class User
    {
        public User()
        {
            Orders = new List<Order>();
            FavoriteStores = new List<string>();
        }

        /// <summary>
        /// The provider code from static <see cref="Providers"/> class.
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// A name to display for the provider.
        /// </summary>
        public string ProviderDisplayName { get; set; }

        /// <summary>
        /// The primary key of the entity.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name to display for the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user's from the provider.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// This role is always set to "User".
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// The ids of the favorite stores the user has.
        /// </summary>
        public ICollection<string> FavoriteStores { get; private set; }

        /// <summary>
        /// The orders of a user.
        /// </summary>
        public ICollection<Order> Orders { get; private set; }
    }
}