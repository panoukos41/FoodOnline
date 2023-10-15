using Core.Abstractions.Requests;
using System.Reflection;

namespace Core.Abstractions.Handlers;

public abstract class QueryHandler<TQuery, TResult> :
    IQueryHandler<TQuery, Result<TResult>>
    where TQuery : Query<TResult>
    where TResult : notnull
{
    public abstract ValueTask<Result<TResult>> Handle(TQuery query, CancellationToken cancellationToken);

    // todo: refactor this
    protected static readonly PropertyInfo IdProp =
        typeof(IEntity).GetProperty(nameof(IEntity.Id), BindingFlags.Public | BindingFlags.Instance)!;
}
