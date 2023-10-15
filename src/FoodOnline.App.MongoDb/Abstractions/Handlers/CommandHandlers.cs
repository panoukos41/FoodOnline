using Core.Abstractions.Handlers;
using FoodOnline.App.MongoDb;
using MongoDB.Driver;

namespace FoodOnline.Abstractions.Handlers;

public abstract class DeleteCommandHandler<TCommand> :
    CommandHandler<TCommand, Void>
    where TCommand : DeleteCommand
{
    private readonly MongoDbContext mongoDb;

    public abstract string Collection { get; }

    protected DeleteCommandHandler(MongoDbContext mongoDb)
    {
        this.mongoDb = mongoDb;
    }

    public sealed override async ValueTask<Result<Void>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var id = command.Id;
        var collection = mongoDb.GetCollection<IEntity>(Collection);

        var filter = Builders<IEntity>.Filter.Eq(x => x.Id, id);
        var result = await collection.DeleteOneAsync(m => m.Id == id, null, cancellationToken);

        return result.DeletedCount is 1
            ? Void.Value
            : Problems.NotFound;
    }
}
