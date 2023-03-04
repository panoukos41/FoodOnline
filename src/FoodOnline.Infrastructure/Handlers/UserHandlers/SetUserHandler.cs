using FoodOnline.Infrastructure.Abstractions.Handlers;
using FoodOnline.Models;
using FoodOnline.Requests.UserRequests;

namespace FoodOnline.Infrastructure.Handlers.UserHandlers;

public sealed class SetUserHandler : SetCommandHandler<SetUser, UserModel>
{
    public override string Collection { get; } = "users";
}
