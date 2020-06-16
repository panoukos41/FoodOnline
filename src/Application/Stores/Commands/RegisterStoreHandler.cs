using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Stores.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Stores.Commands
{
    public class RegisterStoreHandler : IRequestHandler<RegisterStore, string>
    {
        private readonly IApplicationDbContext context;

        public RegisterStoreHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Handle(RegisterStore request, CancellationToken cancellationToken)
        {
            if (!await context.StoreUsers.AnyAsync(x => x.Id == request.OwnerId))
                throw new NotFoundException(nameof(StoreUser), request.OwnerId);

            if (await context.Stores.AnyAsync(x => x.Name == request.Name && x.Address == request.Address))
                throw new StoreExistsException(request.Name, request.Address);

            var store = new Store
            {
                Id = IdGenerator.Generate(),
                Published = false,
                Open = false,
                Name = request.Name,
                Description = "Here set your own description, working days, hours and contanct details like phone email",
                Catalogue = "",
                Address = request.Address,
                OwnerId = request.OwnerId
            };

            context.Stores.Add(store);
            await context.SaveChangesAsync(cancellationToken);
            return store.Id;
        }
    }
}