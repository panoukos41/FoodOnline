using FoodOnline.Domain.Stores.Models;
using MediatR;
using System.Collections.Generic;

namespace FoodOnline.Domain.Stores.Queries
{
    public class GetOwnerStores : IRequest<IEnumerable<StoreListModel>>
    {
        public string OwnerId { get; set; }
    }
}