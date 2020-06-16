using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Stores.Models;
using FoodOnline.Domain.Stores.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Stores.Queries
{
    public class GetStoreHandler : IRequestHandler<GetStore, StoreModel>
    {
        private readonly IApplicationDbContext context;

        public GetStoreHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<StoreModel> Handle(GetStore request, CancellationToken cancellationToken)
        {
            var store = await context.Stores.FindAsync(request.Id)
                ?? throw new NotFoundException(nameof(Store), request.Id);

            return new StoreModel
            {
                Id = store.Id,
                Address = store.Address,
                Catalogue = store.Catalogue,
                Description = store.Description,
                IsOpen = store.Open,
                IsPublished = store.Published,
                Name = store.Name
            };
        }
    }
}