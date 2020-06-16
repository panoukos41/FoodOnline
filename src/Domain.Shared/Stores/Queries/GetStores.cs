using FoodOnline.Domain.Stores.Models;
using MediatR;
using System.Collections.Generic;

namespace FoodOnline.Domain.Stores.Queries
{
    public class GetStores : IRequest<IEnumerable<StoreListModel>>
    {
        public bool IsOpen { get; set; } = true;

        public string Address { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}