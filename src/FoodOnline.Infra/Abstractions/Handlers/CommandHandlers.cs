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

    public virtual async ValueTask<Result<Uuid>> Handle(TCommand command, CancellationToken cancellationToken)
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
    ICommandHandler<TCommand, Result<Void>>
    where TCommand : UpdateCommand<TEntity>
    where TEntity : IEntity
{
    private readonly IMongoCollection<TEntity> col;

    public abstract string Collection { get; }

    public UpdateCommandHandler(IMongoDatabase mongo)
    {
        col = mongo.GetCollection<TEntity>(Collection);
    }

    public virtual async ValueTask<Result<Void>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var entity = command.Entity;
        var find = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

        await col.ReplaceOneAsync(find, command.Entity, cancellationToken: cancellationToken);

        return Void.Value;
    }
}

//public abstract class DeleteCommandHandler<TCommand> :
//    CommandHandler<TCommand, Void>
//    where TCommand : DeleteCommand
//{
//    public override async ValueTask<Result<Void>> Handle(TCommand command, CancellationToken cancellationToken)
//    {
//        var client = new MongoClient("mongodb://admin:password@localhost:3306");
//        var db = client.GetDatabase("test");

//        var id = command.Id;
//        var collection = db.GetCollection<IId>(Collection);

//        await collection.DeleteOneAsync(m => m.Id == id, null, cancellationToken);

//        return Void.Value;
//    }
//}
