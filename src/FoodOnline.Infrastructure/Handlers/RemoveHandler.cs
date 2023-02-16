using FoodOnline.Models;
using FoodOnline.Requests;
using MongoDB.Driver;

namespace FoodOnline.Infrastructure.Handlers;

public abstract class RemoveHandler<TCommand, TModel> :
    AbstractHandler,
    ICommandHandler<TCommand>
    where TCommand : Remove<TModel>
    where TModel : class, IModel
{
    public async ValueTask<Unit> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var client = new MongoClient("mongodb://admin:password@localhost:3306");
        var db = client.GetDatabase("test");

        var id = command.Id;
        var collection = db.GetCollection<TModel>(Collection);

        await collection.DeleteOneAsync(m => m.Id == id, null, cancellationToken);

        return Unit.Value;
    }
}
