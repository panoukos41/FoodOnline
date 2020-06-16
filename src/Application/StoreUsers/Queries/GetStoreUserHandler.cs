using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.StoreUsers.Models;
using FoodOnline.Domain.StoreUsers.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.StoreUsers.Queries
{
    public class GetStoreUserHandler : IRequestHandler<GetStoreUser, StoreUserModel>
    {
        private readonly IApplicationDbContext context;

        public GetStoreUserHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<StoreUserModel> Handle(GetStoreUser request, CancellationToken cancellationToken)
        {
            var storeUser = await context.StoreUsers.FindAsync(request.Id)
                ?? throw new NotFoundException(nameof(StoreUser), request);

            return new StoreUserModel
            {
                Id = storeUser.Id,
                Role = storeUser.Role,
                Username = storeUser.Username
            };
        }
    }
}