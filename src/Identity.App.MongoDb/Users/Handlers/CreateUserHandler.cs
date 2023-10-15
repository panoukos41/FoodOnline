using FoodOnline.Abstractions.Handlers;
using FoodOnline.Users.Requests;
using MongoDB.Driver;

namespace FoodOnline.Users.Handlers;

public sealed class CreateUserHandler : EntityCommandHandler<CreateUser, User>
{
    public override string CollectionName => "users";

    public CreateUserHandler(IMongoDatabase mongo) : base(mongo)
    {
    }
}
