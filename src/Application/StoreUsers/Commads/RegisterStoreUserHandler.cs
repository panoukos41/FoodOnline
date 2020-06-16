using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.StoreUsers.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.StoreUsers.Commads
{
    public class RegisterStoreUserHandler : IRequestHandler<RegisterStoreUser, string>
    {
        private IApplicationDbContext context;

        public RegisterStoreUserHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Handle(RegisterStoreUser request, CancellationToken cancellationToken)
        {
            if (await context.StoreUsers.AnyAsync(x => x.Username == request.Username))
                throw new UsernameExistsException(nameof(StoreUser), request.Username);

            if (request.IsOwner)
            {
                PasswordHelper.CreatePasswordHash(
                    request.Password,
                    out string hash,
                    out string salt);

                var owner = new StoreUser
                {
                    Id = IdGenerator.Generate(),
                    Username = request.Username,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    Role = Role.StoreOwner
                };

                context.StoreUsers.Add(owner);
                await context.SaveChangesAsync(cancellationToken);
                return owner.Id;
            }
            else
            {
                if (!context.Stores.Any(x => x.Id == request.StoreId))
                    throw new NotFoundException(nameof(Store), request.StoreId);

                PasswordHelper.CreatePasswordHash(
                    request.Password,
                    out string hash,
                    out string salt);

                var employee = new StoreUser
                {
                    Id = IdGenerator.Generate(),
                    Username = request.Username,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    Role = Role.StoreEmployee,
                    StoreId = request.StoreId
                };

                context.StoreUsers.Add(employee);
                await context.SaveChangesAsync(cancellationToken);
                return employee.Id;
            }
        }
    }
}