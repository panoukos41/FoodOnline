using FoodOnline.Domain.StoreUsers.Models;
using MediatR;
using System.Collections.Generic;

namespace FoodOnline.Domain.StoreUsers.Queries
{
    public class GetStoreUsers : IRequest<IEnumerable<StoreUserModel>>
    {
        public string Role { get; set; }
    }
}