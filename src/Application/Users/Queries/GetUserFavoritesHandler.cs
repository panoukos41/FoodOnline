using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Stores.Models;
using FoodOnline.Domain.Users.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Users.Queries
{
    // todo
    public class GetUserFavoritesHandler : IRequestHandler<GetUserFavorites, IEnumerable<StoreListModel>>
    {
        private readonly IApplicationDbContext context;

        public GetUserFavoritesHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StoreListModel>> Handle(GetUserFavorites request, CancellationToken cancellationToken)
        {
            var user = context.Users.Find(request.Id)
                ?? throw new NotFoundException(nameof(User), request.Id);

            var stores = context.Stores.AsNoTracking()
                .Where(l => user.FavoriteStores.Any(id => id == l.Id));

            var toRemove = stores
                .Select(x => x.Id)
                .Where(id => !user.FavoriteStores.Contains(id));

            if (toRemove.Count() != 0)
            {
                foreach (var item in toRemove)
                {
                    user.FavoriteStores.Remove(item);
                }
                await context.SaveChangesAsync(cancellationToken);
            }

            return await stores
                .Select(store => new StoreListModel
                {
                    Id = store.Id,
                    Address = store.Address,
                    IsOpen = store.Open,
                    Name = store.Name
                })
                .ToListAsync();
        }
    }
}