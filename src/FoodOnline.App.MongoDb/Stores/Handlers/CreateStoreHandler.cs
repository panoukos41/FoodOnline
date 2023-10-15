using FoodOnline.App.MongoDb;
using FoodOnline.Stores.Requests;
using MongoDB.Driver;

namespace FoodOnline.Stores.Handlers;

public sealed class CreateStoreHandler : CommandHandler<CreateStore, Store, CreatedResponse>
{
    private readonly StoresAppModule module;
    private readonly MongoDbContext mongoDb;

    public CreateStoreHandler(StoresAppModule module, MongoDbContext mongoDb)
    {
        this.module = module;
        this.mongoDb = mongoDb;
    }

    public override async ValueTask<Result<CreatedResponse>> Handle(CreateStore command, CancellationToken cancellationToken)
    {
        var collection = mongoDb.GetCollection<Store>(module.CollectionName);

        var find = Builders<Store>.Filter.Eq(x => x.Id, Uuid.NewUuid()); // use requestId maybe use cache too.
        var requestFulfilled = await collection.Find(find).AnyAsync(cancellationToken);

        if (requestFulfilled)
        {
            return Problems.Conflict;
        }

        var newId = Uuid.NewUuid();
        var store = command.Data;

        IdProp.SetValue(store, newId); // todo: Improve

        await collection.InsertOneAsync(store, null, cancellationToken);

        return new CreatedResponse(newId);
    }
}
