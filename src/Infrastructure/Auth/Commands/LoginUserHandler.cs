using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain;
using FoodOnline.Domain.Auth.Requests;
using FoodOnline.Domain.Users.Models;
using FoodOnline.Infrastructure.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FoodOnline.Infrastructure.Auth.Commands
{
    public class LoginUserHandler : RequestHandler<LoginUser, UserModel>
    {
        private readonly FoodOnlineUserManager manager;
        private readonly IApplicationDbContext context;

        public LoginUserHandler(FoodOnlineUserManager manager, IApplicationDbContext context)
        {
            this.manager = manager;
            this.context = context;
        }

        protected override UserModel Handle(LoginUser request)
        {
            var user = manager.Authenticate(request.Email, request.Password)
                ?? throw new AuthException();

            var name = context.Users.AsNoTracking()
                .Where(x => x.Id == user.Id)
                .Select(x => x.Name)
                .SingleOrDefault() ?? throw new NotFoundException(nameof(user), user.Id);

            return new UserModel
            {
                Id = user.Id,
                Name = name,
                Email = user.Email,
                ProviderDisplayName = Providers.FoodOnline
            };
        }
    }
}