using Core.Abstractions.Handlers;
using FoodOnline.App.MongoDb;
using MongoDB.Driver;

namespace FoodOnline.Abstractions.Handlers;

public abstract class FindQueryHandler<TQuery, TEntity> :
    QueryHandler<TQuery, TEntity>
    where TQuery : FindQuery<TEntity>
    where TEntity : IEntity
{
    private readonly MongoDbContext mongoDb;

    public abstract string Collection { get; }

    protected FindQueryHandler(MongoDbContext mongoDb)
    {
        this.mongoDb = mongoDb;
    }

    public sealed override async ValueTask<Result<TEntity>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var collection = mongoDb.GetCollection<TEntity>(Collection);

        var filter = Builders<TEntity>.Filter.Eq(x => x.Id, query.Id);
        var result = await collection.Find(filter).SingleOrDefaultAsync(cancellationToken);

        return result is { } ? result : Problems.NotFound;
    }
}
