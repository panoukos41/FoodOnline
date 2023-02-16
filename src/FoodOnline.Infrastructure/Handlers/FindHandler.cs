using FoodOnline.Models;
using FoodOnline.Requests;
using MongoDB.Driver;

namespace FoodOnline.Infrastructure.Handlers;

public abstract class FindHandler<TRequest, TModel> :
    AbstractHandler,
    IRequestHandler<TRequest, TModel>
    where TRequest : Find<TModel>
    where TModel : class, IModel
{
    public async ValueTask<TModel> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var client = new MongoClient("mongodb://admin:password@localhost:3306");
        var db = client.GetDatabase("test");

        var id = request.Id;
        var collection = db.GetCollection<TModel>(Collection);

        var model = await collection.Find(m => m.Id == id, null).FirstOrDefaultAsync(cancellationToken);

        return model;
    }
}

