using FoodOnline.Models;
using FoodOnline.Requests.UserRequests;

namespace FoodOnline.Infrastructure.Handlers.UserHandlers;

public sealed class FindUserHandler :
    FindHandler<FindUser, UserModel>
{
    public override string Collection { get; } = "users";
}
