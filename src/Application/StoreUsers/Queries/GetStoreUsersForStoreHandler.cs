using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.StoreUsers.Models;
using FoodOnline.Domain.StoreUsers.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.StoreUsers.Queries
{
    public class GetStoreUsersForStoreHandler : IRequestHandler<GetStoreUsersrForStore, IEnumerable<StoreUserModel>>
    {
        private readonly IApplicationDbContext context;

        public GetStoreUsersForStoreHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StoreUserModel>> Handle(GetStoreUsersrForStore request, CancellationToken cancellationToken)
        {
            if (!await context.Stores.AnyAsync(x => x.Id == request.StoreId))
                throw new NotFoundException(nameof(Store), request);

            var ownerId = context.Stores.AsNoTracking()
                .Where(store => store.Id == request.StoreId)
                .Select(store => store.OwnerId)
                .SingleOrDefault();

            var users = context.StoreUsers.AsNoTracking()
                .Where(user => user.StoreId == request.StoreId || user.Id == ownerId)
                .Select(user => new StoreUserModel
                {
                    Id = user.Id,
                    Role = user.Role,
                    Username = user.Role
                });

            return await users.ToListAsync();
        }
    }
}