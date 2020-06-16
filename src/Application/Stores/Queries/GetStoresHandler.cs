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
    // Todo Improve!
    public class GetStoresHandler : IRequestHandler<GetStores, IEnumerable<StoreListModel>>
    {
        private readonly IApplicationDbContext context;

        public GetStoresHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StoreListModel>> Handle(GetStores request, CancellationToken cancellationToken)
        {
            request.Country ??= "Ελλάδα";

            return await context.Stores.AsNoTracking()
                .Where(store => store.Published == true)
                .Where(store => store.Open == request.IsOpen)
                .Where(store => store.DeliversTo.Contains(request.Region))
                .Where(store => store.Address.City == request.City)
                .Where(store => store.Address.Country == request.Country)
                .Select(store => new StoreListModel
                {
                    Id = store.Id,
                    Name = store.Name,
                    IsOpen = store.Open,
                    Address = store.Address
                })
                .ToListAsync();
        }
    }
}