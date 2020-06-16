using FoodOnline.Application.Auth.Requests;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain;
using FoodOnline.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Auth.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUser, string>
    {
        private readonly IApplicationDbContext context;

        public CreateUserHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            if (await context.Users.AnyAsync(x => x.Email == request.Email))
                throw new EmailExistsException(nameof(User), request.Email);

            var user = new User
            {
                Id = request.Id,
                LoginProvider = request.LoginProvider,
                ProviderDisplayName = request.ProviderDisplayName,
                Name = request.Name,
                Email = request.Email,
                Role = Role.User
            };

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}