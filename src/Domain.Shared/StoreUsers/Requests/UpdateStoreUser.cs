using MediatR;

namespace FoodOnline.Domain.StoreUsers.Requests
{
    public class UpdateStoreUser : IRequest
    {
        /// <summary>
        /// The user id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Current user password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The new username.
        /// </summary>
        public string NewUsername { get; set; }

        /// <summary>
        /// The new password.
        /// </summary>
        public string NewPassword { get; set; }
    }
}