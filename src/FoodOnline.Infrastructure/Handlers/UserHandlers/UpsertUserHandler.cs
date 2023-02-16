using FoodOnline.Models;
using FoodOnline.Requests.UserRequests;

namespace FoodOnline.Infrastructure.Handlers.UserHandlers;

public sealed class UpsertUserHandler :
    UpsertHandler<UpsertUser, UserModel>
{
    public override string Collection { get; } = "users";
}
