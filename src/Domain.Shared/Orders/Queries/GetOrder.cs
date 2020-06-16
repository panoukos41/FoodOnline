using FoodOnline.Domain.Orders.Models;
using MediatR;

namespace FoodOnline.Domain.Orders.Queries
{
    public class GetOrder : IRequest<OrderModel>
    {
        public string OrderId { get; set; }
    }
}