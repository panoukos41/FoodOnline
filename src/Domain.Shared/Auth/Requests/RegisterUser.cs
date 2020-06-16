using MediatR;

namespace FoodOnline.Domain.Auth.Requests
{
    public class RegisterUser : IRequest<string>
    {
        public virtual string Email { get; set; }

        public virtual string Name { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordConfirmation { get; set; }
    }
}