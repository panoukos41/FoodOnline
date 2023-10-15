using FoodOnline.Abstractions.Handlers;
using FoodOnline.App.MongoDb;
using FoodOnline.Stores.Requests;

namespace FoodOnline.Stores.Handlers;

public sealed class GetStoreHandler : FindQueryHandler<GetStore, Store>
{
    public override string Collection => "stores";

    public GetStoreHandler(MongoDbContext mongoDb) : base(mongoDb)
    {
    }
}
