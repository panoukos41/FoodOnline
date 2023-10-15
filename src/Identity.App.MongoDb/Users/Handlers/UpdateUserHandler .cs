using FoodOnline.Abstractions.Handlers;
using FoodOnline.Users.Requests;
using MongoDB.Driver;

namespace FoodOnline.Users.Handlers;

public sealed class UpdateUserHandler : UpdateCommandHandler<UpdateUser, User>
{
    public override string Collection => "users";

    public UpdateUserHandler(IMongoDatabase mongo) : base(mongo)
    {
    }
}
