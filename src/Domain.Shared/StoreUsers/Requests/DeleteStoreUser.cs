using MediatR;

namespace FoodOnline.Domain.StoreUsers.Requests
{
    public class DeleteStoreUser : IRequest
    {
        public string Id { get; set; }
    }
}