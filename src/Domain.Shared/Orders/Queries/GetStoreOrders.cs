using FoodOnline.Domain.Orders.Models;
using MediatR;

namespace FoodOnline.Domain.Orders.Queries
{
    // todo
    public class GetStoreOrders : IRequest<StoreOrderModel>
    {
        public string StoreId { get; set; }
    }
}