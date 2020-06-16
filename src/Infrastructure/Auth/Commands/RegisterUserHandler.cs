using FoodOnline.Domain.Auth.Requests;
using FoodOnline.Infrastructure.Identity;
using MediatR;

namespace FoodOnline.Infrastructure.Auth.Commands
{
    public class RegisterUserHandler : RequestHandler<RegisterUser, string>
    {
        private readonly FoodOnlineUserManager manager;

        public RegisterUserHandler(FoodOnlineUserManager manager)
        {
            this.manager = manager;
        }

        protected override string Handle(RegisterUser request)
        {
            return manager.Create(request.Name, request.Email, request.Password);
        }
    }
}