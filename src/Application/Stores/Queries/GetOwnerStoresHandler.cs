using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Stores.Models;
using FoodOnline.Domain.Stores.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Stores.Queries
{
    public class GetOwnerStoresHandler : IRequestHandler<GetOwnerStores, IEnumerable<StoreListModel>>
    {
        private readonly IApplicationDbContext context;

        public GetOwnerStoresHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StoreListModel>> Handle(GetOwnerStores request, CancellationToken cancellationToken)
        {
            return await context.Stores.AsNoTracking()
                .Where(x => x.OwnerId == request.OwnerId)
                .Select(store => new StoreListModel
                {
                    Address = store.Address,
                    Id = store.Id,
                    IsOpen = store.Open,
                    Name = store.Name
                })
                .ToListAsync();
        }
    }
}