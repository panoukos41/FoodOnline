using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Stores.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Stores.Commands
{
    public class UpdatePublishStoreHandler : AsyncRequestHandler<UpdatePublishStore>
    {
        private readonly IApplicationDbContext context;

        public UpdatePublishStoreHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        protected override async Task Handle(UpdatePublishStore request, CancellationToken cancellationToken)
        {
            var store = await context.Stores.FindAsync(request.Id)
                ?? throw new NotFoundException(nameof(Store), request.Id);

            store.Published = request.Publish;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}