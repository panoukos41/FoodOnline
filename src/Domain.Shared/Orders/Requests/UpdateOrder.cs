using FoodOnline.Domain.Enums;
using MediatR;

namespace FoodOnline.Domain.Orders.Commands
{
    public class UpdateOrder : IRequest
    {
        public string OrderId { get; set; }

        public string ConnectionId { get; set; }

        public OrderState OrderState { get; set; }
    }
}