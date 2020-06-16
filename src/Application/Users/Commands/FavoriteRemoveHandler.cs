using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Users.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Users.Commands
{
    public class FavoriteRemoveHandler : AsyncRequestHandler<FavoriteRemove>
    {
        private readonly IApplicationDbContext context;

        public FavoriteRemoveHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        protected override async Task Handle(FavoriteRemove request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FindAsync(request.UserId)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            user.FavoriteStores.Remove(request.StoreId);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}