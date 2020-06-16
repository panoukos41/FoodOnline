using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.StoreUsers.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FoodOnline.Application.StoreUsers.Commads
{
    public class AuthStoreUserHandler : RequestHandler<AuthStoreUser, (string id, string role, string storeId)>
    {
        private readonly IApplicationDbContext context;

        public AuthStoreUserHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        protected override (string id, string role, string storeId) Handle(AuthStoreUser request)
        {
            var user = context.StoreUsers
                .AsNoTracking()
                .SingleOrDefault(x => x.Username == request.Username)
                ?? throw new NotFoundException(nameof(StoreUser), request.Username);

            if (PasswordHelper.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return (user.Id, user.Role, user.StoreId);
            }
            throw new AuthException();
        }
    }
}