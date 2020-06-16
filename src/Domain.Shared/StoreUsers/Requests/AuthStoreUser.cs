using MediatR;

namespace FoodOnline.Domain.StoreUsers.Requests
{
    public class AuthStoreUser : IRequest<(string, string, string)>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}