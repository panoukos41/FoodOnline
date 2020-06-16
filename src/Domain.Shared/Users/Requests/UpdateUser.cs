using MediatR;

namespace FoodOnline.Domain.Users.Requests
{
    public class UpdateUser : IRequest
    {
        public string Id { get; set; }

        public string NewName { get; set; }
    }
}