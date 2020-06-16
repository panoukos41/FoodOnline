using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.StoreUsers.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.StoreUsers.Commads
{
    public class DeleteStoreUserHandler : AsyncRequestHandler<DeleteStoreUser>
    {
        private readonly IApplicationDbContext context;

        public DeleteStoreUserHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        protected async override Task Handle(DeleteStoreUser request, CancellationToken cancellationToken)
        {
            if (!await context.StoreUsers.AnyAsync(x => x.Id == request.Id))
                throw new NotFoundException(nameof(StoreUser), request.Id);

            context.Entry(new StoreUser { Id = request.Id }).State = EntityState.Deleted;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}