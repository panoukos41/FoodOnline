using FoodOnline.Infrastructure.Abstractions.Handlers;
using FoodOnline.Requests.UserRequests;

namespace FoodOnline.Infrastructure.Handlers.UserHandlers;

public sealed class DeleteUserHandler : DeleteCommandHandler<DeleteUser>
{
    public override string Collection { get; } = "users";
}
