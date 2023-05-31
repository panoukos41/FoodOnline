using FoodOnline.Abstractions.Handlers;
using FoodOnline.Users.Requests;
using MongoDB.Driver;

namespace FoodOnline.Users.Handlers;

public sealed class CreateUserHandler : CreateCommandHandler<CreateUser, User>
{
    public override string Collection => "users";

    public CreateUserHandler(IMongoDatabase mongo) : base(mongo)
    {
    }
}
