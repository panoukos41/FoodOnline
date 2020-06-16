using MediatR;

namespace FoodOnline.Application.Auth.Requests
{
    public class CreateUser : IRequest<string>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderDisplayName { get; set; }
    }
}