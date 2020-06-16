using FoodOnline.Domain.Orders.Models;
using MediatR;

namespace FoodOnline.Domain.Orders.Queries
{
    // todo
    public class GetUserOrders : IRequest<UserOrderModel>
    {
        public string UserId { get; set; }
    }
}