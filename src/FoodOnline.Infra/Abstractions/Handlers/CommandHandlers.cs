using MongoDB.Driver;
using System.Reflection;

namespace FoodOnline.Abstractions.Handlers;

public abstract class CommandHandler<TCommand, TResult> :
    ICommandHandler<TCommand, Result<TResult>>
    where TCommand : Requests.Command<TResult>
    where TResult : notnull
{
    public abstract ValueTask<Result<TResult>> Handle(TCommand command, CancellationToken cancellationToken);
}

public abstract class CreateCommandHandler<TCommand, TEntity> :
    ICommandHandler<TCommand, Result<Uuid>>
    where TCommand : CreateCommand<TEntity>
    where TEntity : IEntity
{
    private readonly IMongoCollection<TEntity> col;

    public abstract string Collection { get; }

    public CreateCommandHandler(IMongoDatabase mongo)
    {
        col = mongo.GetCollection<TEntity>(Collection);
    }

    public async ValueTask<Result<Uuid>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var entity = command.Entity;
        idProp.SetValue(entity, Uuid.NewUuid());

        await col.InsertOneAsync(entity, cancellationToken: cancellationToken);

        return entity.Id;
    }

    private static readonly PropertyInfo idProp =
        typeof(TEntity).GetProperty(nameof(IEntity.Id), BindingFlags.Public | BindingFlags.Instance)!;
}

public abstract class UpdateCommandHandler<TCommand, TEntity> :
    ICommandHandler<TCommand, Result<None>>
    where TCommand : UpdateCommand<TEntity>
    where TEntity : IEntity
{
    private readonly IMongoCollection<TEntity> col;

    public abstract string Collection { get; }

    public UpdateCommandHandler(IMongoDatabase mongo)
    {
        col = mongo.GetCollection<TEntity>(Collection);
    }

    public async ValueTask<Result<None>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var entity = command.Entity;
        var find = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

        await col.ReplaceOneAsync(find, command.Entity, cancellationToken: cancellationToken);

        return None.Value;
    }
}

//public abstract class SetCommandHandler<TCommand, T> :
//    CommandHandler<TCommand, T>
//    where TCommand : SetCommand<T>
//    where T : notnull, IEntity
//{
//    public override async ValueTask<Result<T>> Handle(TCommand command, CancellationToken cancellationToken)
//    {
//        var client = new MongoClient("mongodb://admin:password@localhost:3306");
//        var db = client.GetDatabase("test");

//        var model = command.Entity;
//        var collection = db.GetCollection<T>(Collection);

//        await collection.InsertOneAsync(model, null, cancellationToken);

//        return model;
//    }
//}

//public abstract class DeleteCommandHandler<TCommand> :
//    CommandHandler<TCommand, None>
//    where TCommand : DeleteCommand
//{
//    public override async ValueTask<Result<None>> Handle(TCommand command, CancellationToken cancellationToken)
//    {
//        var client = new MongoClient("mongodb://admin:password@localhost:3306");
//        var db = client.GetDatabase("test");

//        var id = command.Id;
//        var collection = db.GetCollection<IId>(Collection);

//        await collection.DeleteOneAsync(m => m.Id == id, null, cancellationToken);

//        return None.Value;
//    }
//}
