using FoodOnline.Abstractions.Handlers;

namespace FoodOnline.Users.Requests;

public sealed class UpdateUserHandler : UpdateCommandHandler<UpdateUser, User>
{
    public override string Collection { get; } = "AAAA";
}
