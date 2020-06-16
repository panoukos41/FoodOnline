using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Stores.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Stores.Commands
{
    public class UpdateOpenStoreHandler : AsyncRequestHandler<UpdateOpenStore>
    {
        private readonly IApplicationDbContext context;

        public UpdateOpenStoreHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        protected override async Task Handle(UpdateOpenStore request, CancellationToken cancellationToken)
        {
            var store = await context.Stores.FindAsync(request.Id)
                ?? throw new NotFoundException(nameof(Store), request.Id);

            store.Open = request.Open;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}