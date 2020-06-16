using FoodOnline.Domain.StoreUsers.Models;
using MediatR;

namespace FoodOnline.Domain.StoreUsers.Queries
{
    public class GetStoreUser : IRequest<StoreUserModel>
    {
        public string Id { get; set; }
    }
}