using FoodOnline.Domain.Stores.Models;
using MediatR;

namespace FoodOnline.Domain.Stores.Queries
{
    public class GetStore : IRequest<StoreModel>
    {
        public string Id { get; set; }
    }
}