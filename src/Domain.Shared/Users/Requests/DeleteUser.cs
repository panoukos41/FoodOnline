using MediatR;

namespace FoodOnline.Domain.Users.Requests
{
    public class DeleteUser : IRequest
    {
        public string Id { get; set; }
    }
}