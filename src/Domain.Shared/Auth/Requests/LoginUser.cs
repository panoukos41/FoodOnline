using FoodOnline.Domain.Users.Models;
using MediatR;

namespace FoodOnline.Domain.Auth.Requests
{
    public class LoginUser : IRequest<UserModel>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}