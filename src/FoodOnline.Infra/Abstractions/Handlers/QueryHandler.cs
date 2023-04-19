namespace FoodOnline.Abstractions.Handlers;

public abstract class QueryHandler<TQuery, T> :
    AbstractHandler,
    IQueryHandler<TQuery, Result<T>>
    where TQuery : Query<T>
    where T : notnull
{
    public abstract ValueTask<Result<T>> Handle(TQuery query, CancellationToken cancellationToken);
}
