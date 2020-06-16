using FoodOnline.Domain.StoreUsers.Models;
using MediatR;
using System.Collections.Generic;

namespace FoodOnline.Domain.StoreUsers.Queries
{
    public class GetStoreUsersrForStore : IRequest<IEnumerable<StoreUserModel>>
    {
        public string StoreId { get; set; }
    }
}