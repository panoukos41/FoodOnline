using MediatR;

namespace FoodOnline.Domain.Stores.Requests
{
    public class UpdateOpenStore : IRequest
    {
        public string Id { get; set; }

        public bool Open { get; set; }
    }
}