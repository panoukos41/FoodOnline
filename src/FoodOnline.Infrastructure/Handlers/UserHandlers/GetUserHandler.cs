using FoodOnline.Infrastructure.Abstractions.Handlers;
using FoodOnline.Models;
using FoodOnline.Requests.UserRequests;

namespace FoodOnline.Infrastructure.Handlers.UserHandlers;

public sealed class GetUserHandler : GetRequestHandler<GetUser, UserModel>
{
    public override string Collection { get; } = "users";
}
