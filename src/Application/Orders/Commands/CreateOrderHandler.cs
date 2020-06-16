using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Orders.Commands;
using FoodOnline.Domain.Orders.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Orders.Commands
{
    public class CreateOrderHandler : IRequestHandler<CreateOrder, NewOrderModel>
    {
        private readonly IApplicationDbContext context;

        public CreateOrderHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<NewOrderModel> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            if (await context.Orders.AnyAsync(x => x.IdempotencyToken == request.IdempotencyToken))
            {
                throw new OrderExistsException(request.IdempotencyToken);
            }

            if (!string.IsNullOrWhiteSpace(request.UserId)
                && !await context.Users.AnyAsync(x => x.Id == request.UserId))
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            if (!await context.Stores.AnyAsync(x => x.Id == request.StoreId))
            {
                throw new NotFoundException(nameof(Store), request.StoreId);
            }

            var order = new Order
            {
                Id = IdGenerator.Generate(),
                IdempotencyToken = request.IdempotencyToken,
                Entries = request.Entries,
                TotalPriceEur = request.TotalPriceEur,
                Address = request.Address,
                StoreId = request.StoreId,
                UserId = request.UserId
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync(cancellationToken);
            return new NewOrderModel
            {
                OrderId = order.Id,
                Address = order.Address,
                Entries = order.Entries,
                TotalPriceEur = order.TotalPriceEur
            };
        }
    }
}