using FoodOnline.Abstractions.Handlers;
using FoodOnline.Users.Requests;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnline.Users.Handlers;

public sealed class GetUserHandler : GetQueryHandler<GetUser, User>
{
    public override string Collection => "users";

    public GetUserHandler(IMongoDatabase mongo) : base(mongo)
    {
    }
}
