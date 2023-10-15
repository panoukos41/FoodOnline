using Core.Abstractions.Requests;
using System.Reflection;

namespace Core.Abstractions.Handlers;

public abstract class CommandHandler<TCommand, TResult> :
    ICommandHandler<TCommand, Result<TResult>>
    where TCommand : Command<TResult>
    where TResult : notnull
{
    public abstract ValueTask<Result<TResult>> Handle(TCommand command, CancellationToken cancellationToken);

    // todo: Refactor this
    protected static readonly PropertyInfo IdProp =
        typeof(IEntity).GetProperty(nameof(IEntity.Id), BindingFlags.Public | BindingFlags.Instance)!;
}

public abstract class CommandHandler<TCommand, TData, TResult> :
    CommandHandler<TCommand, TResult>
    where TCommand : Command<TData, TResult>
    where TData : notnull, IValid
    where TResult : notnull
{
}
