using FoodOnline.Abstractions;
using FoodOnline.Abstractions.Requests;
using FoodOnline.Models;
using MongoDB.Driver;

namespace FoodOnline.Infrastructure.Abstractions.Handlers;

public abstract class CommandHandler<TCommand, T> :
    AbstractHandler,
    ICommandHandler<TCommand, Result<T>>
    where TCommand : FoodOnline.Abstractions.Requests.Command<T>
    where T : notnull
{
    public abstract ValueTask<Result<T>> Handle(TCommand command, CancellationToken cancellationToken);
}

public abstract class SetCommandHandler<TCommand, T> :
    CommandHandler<TCommand, T>
    where TCommand : SetCommand<T>
    where T : notnull, IModel
{
    public override async ValueTask<Result<T>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var client = new MongoClient("mongodb://admin:password@localhost:3306");
        var db = client.GetDatabase("test");

        var model = command.Model;
        var collection = db.GetCollection<T>(Collection);

        await collection.InsertOneAsync(model, null, cancellationToken);

        return model;
    }
}

public abstract class DeleteCommandHandler<TCommand> :
    CommandHandler<TCommand, Unit>
    where TCommand : DeleteCommand
{
    public override async ValueTask<Result<Unit>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var client = new MongoClient("mongodb://admin:password@localhost:3306");
        var db = client.GetDatabase("test");

        var id = command.Id;
        var collection = db.GetCollection<IId>(Collection);

        await collection.DeleteOneAsync(m => m.Id == id, null, cancellationToken);

        return Unit.Value;
    }
}
