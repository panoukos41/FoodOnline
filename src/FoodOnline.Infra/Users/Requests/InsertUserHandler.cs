using FoodOnline.Abstractions.Handlers;

namespace FoodOnline.Users.Requests;

public sealed class InsertUserHandler : InsertCommandHandler<InsertUser, User>
{
    public override string Collection { get; } = "AAAA";
}
