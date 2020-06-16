using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.StoreUsers.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.StoreUsers.Commads
{
    public class UpdateStoreUserHandler : AsyncRequestHandler<UpdateStoreUser>
    {
        private readonly IApplicationDbContext context;

        public UpdateStoreUserHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        protected override async Task Handle(UpdateStoreUser request, CancellationToken cancellationToken)
        {
            if (await context.StoreUsers.AnyAsync(x => x.Username == request.NewUsername))
                throw new UsernameExistsException(nameof(StoreUser), request.NewUsername);

            var storeUser = await context.StoreUsers.FindAsync(request.Id)
                ?? throw new NotFoundException(nameof(StoreUser), request.Id);

            if (!PasswordHelper.VerifyPassword(request.Password, storeUser.PasswordHash, storeUser.PasswordSalt))
                throw new PasswordInvalidException(nameof(StoreUser), request.Id);

            if (!string.IsNullOrWhiteSpace(request.NewUsername))
            {
                storeUser.Username = request.NewUsername;
            }

            if (!string.IsNullOrWhiteSpace(request.NewPassword))
            {
                PasswordHelper.CreatePasswordHash(
                    request.NewPassword,
                    out string hash,
                    out string salt);

                storeUser.PasswordHash = hash;
                storeUser.PasswordSalt = salt;
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}