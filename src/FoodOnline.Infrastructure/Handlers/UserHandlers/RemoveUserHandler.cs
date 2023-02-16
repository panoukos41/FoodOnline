using FoodOnline.Models;
using FoodOnline.Requests.UserRequests;

namespace FoodOnline.Infrastructure.Handlers.UserHandlers;

public sealed class RemoveUserHandler :
    RemoveHandler<RemoveUser, UserModel>
{
    public override string Collection { get; } = "users";
}
