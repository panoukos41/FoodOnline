using FoodOnline.Models;
using FoodOnline.Requests;
using MongoDB.Driver;

namespace FoodOnline.Infrastructure.Handlers;

public abstract class UpsertHandler<TCommand, TModel> :
    AbstractHandler,
    ICommandHandler<TCommand, TModel>
    where TCommand : Upsert<TModel>
    where TModel : class, IModel
{
    public async ValueTask<TModel> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var client = new MongoClient("mongodb://admin:password@localhost:3306");
        var db = client.GetDatabase("test");

        var model = command.Model;
        var collection = db.GetCollection<TModel>(Collection);

        await collection.InsertOneAsync(model, null, cancellationToken);

        return model;
    }
}
