using FoodOnline.Domain.Stores.Models;
using MediatR;
using System.Collections.Generic;

namespace FoodOnline.Domain.Users.Queries
{
    public class GetUserFavorites : IRequest<IEnumerable<StoreListModel>>
    {
        public string Id { get; set; }
    }
}