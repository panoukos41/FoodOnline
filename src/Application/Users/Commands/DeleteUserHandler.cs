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
    public class DeleteUserHandler : AsyncRequestHandler<DeleteUser>
    {
        private readonly IApplicationDbContext context;

        public DeleteUserHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        protected override async Task Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            if (!await context.Users.AnyAsync(x => x.Id == request.Id))
                throw new NotFoundException(nameof(User), request.Id);

            context.Entry(new User { Id = request.Id }).State = EntityState.Deleted;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}