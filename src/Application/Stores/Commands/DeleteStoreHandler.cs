using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Stores.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Stores.Commands
{
    public class DeleteStoreHandler : AsyncRequestHandler<DeleteStore>
    {
        private readonly IApplicationDbContext context;

        public DeleteStoreHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        protected async override Task Handle(DeleteStore request, CancellationToken cancellationToken)
        {
            if (!context.Stores.Any(x => x.Id == request.Id))
                throw new NotFoundException(nameof(Store), request.Id);

            context.Entry(new Store { Id = request.Id }).State = EntityState.Deleted;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}