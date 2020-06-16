using MediatR;

namespace FoodOnline.Domain.Stores.Requests
{
    public class UpdatePublishStore : IRequest
    {
        public string Id { get; set; }

        public bool Publish { get; set; }
    }
}