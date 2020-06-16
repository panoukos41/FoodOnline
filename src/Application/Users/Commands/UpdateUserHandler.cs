using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Users.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Users.Commands
{
    public class UpdateUserHandler : AsyncRequestHandler<UpdateUser>
    {
        private readonly IApplicationDbContext context;

        public UpdateUserHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        protected override async Task Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FindAsync(request.Id)
                ?? throw new NotFoundException(nameof(User), request.Id);

            if (string.IsNullOrWhiteSpace(request.NewName))
            {
                user.Name = request.NewName;
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}