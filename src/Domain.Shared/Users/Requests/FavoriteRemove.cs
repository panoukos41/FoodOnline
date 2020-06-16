using MediatR;

namespace FoodOnline.Domain.Users.Requests
{
    public class FavoriteRemove : IRequest
    {
        public string UserId { get; set; }

        public string StoreId { get; set; }
    }
}