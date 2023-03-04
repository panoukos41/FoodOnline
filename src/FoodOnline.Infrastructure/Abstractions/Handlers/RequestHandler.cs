using FoodOnline.Abstractions;
using FoodOnline.Abstractions.Requests;
using MongoDB.Driver;

namespace FoodOnline.Infrastructure.Abstractions.Handlers;

public abstract class RequestHandler<TRequest, T> :
    AbstractHandler,
    IRequestHandler<TRequest, Result<T>>
    where TRequest : Request<T>
    where T : notnull
{
    public abstract ValueTask<Result<T>> Handle(TRequest request, CancellationToken cancellationToken);
}

public abstract class GetRequestHandler<TRequest, T> :
    RequestHandler<TRequest, T>
    where TRequest : GetRequest<T>
    where T : notnull, IId
{

    public override async ValueTask<Result<T>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var client = new MongoClient("mongodb://admin:password@localhost:3306");
        var db = client.GetDatabase("test");

        var id = request.Id;
        var collection = db.GetCollection<T>(Collection);

        var model = await collection.Find(m => m.Id == id, null).FirstOrDefaultAsync(cancellationToken);

        return model is { } ? model : Errors.NotFound with
        {
            Reason = $"The '{typeof(T).Name}' with Id '{id}' could not be found."
        };
    }
}
