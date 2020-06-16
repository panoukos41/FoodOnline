using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Users.Models;
using FoodOnline.Domain.Users.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Users.Queries
{
    public class GetUserHandler : IRequestHandler<GetUser, UserModel>
    {
        private readonly IApplicationDbContext context;

        public GetUserHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<UserModel> Handle(GetUser request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FindAsync(request.Id);
            if (user == null) throw new NotFoundException(nameof(User), request.Id);

            return new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                ProviderDisplayName = user.ProviderDisplayName
            };
        }
    }
}