using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Stores.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Stores.Commands
{
    public class UpdateStoreHandler : AsyncRequestHandler<UpdateStore>
    {
        private readonly IApplicationDbContext context;

        public UpdateStoreHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        protected override async Task Handle(UpdateStore request, CancellationToken cancellationToken)
        {
            var store = await context.Stores.FindAsync(request.Id)
                ?? throw new NotFoundException(nameof(Store), request.Id);

            store.Published = request.Published;
            store.Open = request.Open;

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                store.Description = request.Description;
            }

            if (!string.IsNullOrEmpty(request.Catalogue))
            {
                store.Catalogue = request.Catalogue;
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}