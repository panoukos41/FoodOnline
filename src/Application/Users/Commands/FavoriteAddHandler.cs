using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Users.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Users.Commands
{
    public class FavoriteAddHandler : AsyncRequestHandler<FavoriteAdd>
    {
        private readonly IApplicationDbContext context;

        public FavoriteAddHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        protected override async Task Handle(FavoriteAdd request, CancellationToken cancellationToken)
        {
            if (!await context.Stores.AnyAsync(x => x.Id == request.StoreId))
                throw new NotFoundException(nameof(Store), request.StoreId);

            var user = await context.Users.FindAsync(request.UserId)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            user.FavoriteStores.Add(request.StoreId);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}