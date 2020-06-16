using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Orders.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Orders.Commands
{
    public class UpdateOrderHandler : AsyncRequestHandler<UpdateOrder>
    {
        private readonly IApplicationDbContext context;

        public UpdateOrderHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        protected override async Task Handle(UpdateOrder request, CancellationToken cancellationToken)
        {
            var order = await context.Orders.FindAsync(request.OrderId)
                ?? throw new NotFoundException(nameof(Order), request.OrderId);

            order.State = request.OrderState;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}