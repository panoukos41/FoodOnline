using MediatR;

namespace FoodOnline.Domain.Stores.Requests
{
    public class DeleteStore : IRequest
    {
        public string Id { get; set; }
    }
}