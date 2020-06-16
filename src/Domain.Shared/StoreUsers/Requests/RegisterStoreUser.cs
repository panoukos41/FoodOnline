using MediatR;

namespace FoodOnline.Domain.StoreUsers.Requests
{
    public class RegisterStoreUser : IRequest<string>
    {
        /// <summary>
        /// The employee username used to login.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password to use.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// If true the StoreId property is ignored.
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// The id of the store in which this employee belongs.
        /// </summary>
        public string StoreId { get; set; }
    }
}