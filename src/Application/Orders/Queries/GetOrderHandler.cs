using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.Orders.Models;
using FoodOnline.Domain.Orders.Queries;
using FoodOnline.Domain.Stores.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Orders.Queries
{
    public class GetOrderHandler : IRequestHandler<GetOrder, OrderModel>
    {
        private readonly IApplicationDbContext context;

        public GetOrderHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<OrderModel> Handle(GetOrder request, CancellationToken cancellationToken)
        {
            var order = await context.Orders.AsNoTracking()
                .Where(order => order.Id == request.OrderId)
                .Include(x => x.Store)
                .SingleOrDefaultAsync() ?? throw new NotFoundException(nameof(Order), request.OrderId);

            return new OrderModel
            {
                State = order.State,
                Address = order.Address,
                TotalPriceEur = order.TotalPriceEur,
                Entries = order.Entries,
                Store = new StoreListModel
                {
                    Name = order.Store?.Name ?? "No longer available",
                    Address = order.Store?.Address ?? null,
                    Id = order.Store?.Id
                }
            };
        }
    }
}