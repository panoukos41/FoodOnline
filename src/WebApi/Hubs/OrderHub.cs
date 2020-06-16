using FoodOnline.Domain;
using FoodOnline.Domain.Enums;
using FoodOnline.Domain.Orders.Commands;
using FoodOnline.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FoodOnline.WebApi.Hubs
{
    public class OrderHub : Hub
    {
        private readonly IMediator Mediator;

        public OrderHub(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task PlaceOrder(string connectionId, CreateOrder order, string address)
        {
            order.Address = Address.For(address);
            var newOrder = await Mediator.Send(order);
            newOrder.ConnectionId = connectionId;
            newOrder.Address = null;
            await Clients.Groups(order.StoreId).SendAsync("NewOrder", newOrder, address);
            await Clients.Caller.SendAsync("UpdateOrder", OrderState.Received);
        }

        //[Authorize(Policy = Policy.StaffAndCustomer)]
        public async Task UpdateOrder(UpdateOrder update)
        {
            await Mediator.Send(update);
            await Clients.Client(update.ConnectionId).SendAsync("UpdateOrder", update.OrderState);
        }

        //[Authorize(Policy = Policy.StaffAndCustomer)]
        public async Task Connect(string storeId, string connectionId)
        {
            await Groups.AddToGroupAsync(connectionId, storeId);
        }

        //[Authorize(Policy = Policy.StaffAndCustomer)]
        public async Task Disconnect(string storeId, string connectionId)
        {
            await Groups.RemoveFromGroupAsync(connectionId, storeId);
        }
    }
}