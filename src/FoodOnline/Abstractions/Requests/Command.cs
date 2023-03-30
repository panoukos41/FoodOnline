using FluentValidation;

namespace FoodOnline.Abstractions.Requests;

/// <summary>
/// Represents a POST request.
/// </summary>
/// <typeparam name="T">The type of the result object.</typeparam>
public abstract record Command<T> : ICommand<Result<T>> where T : notnull
{
}

/// <summary>
/// Represents a POST request like <see cref="Command{T}"/> but
/// also requires an <see cref="IEntity"/> to send and provides validation.
/// </summary>
/// <typeparam name="T">The type of the result object.</typeparam>
public abstract record SetCommand<TEntity> : Command<TEntity>, IValid where TEntity : notnull, IEntity
{
    public TEntity Entity { get; }

    protected SetCommand(TEntity entity)
    {
        Entity = entity;
    }

    public static IValidator Validator { get; } = new InlineValidator<SetCommand<TEntity>>
    {
        static v => v.RuleFor(x => x.Entity).SetValidator((IValidator<TEntity>)TEntity.Validator)
    };
}

/// <summary>
/// Represents a DELETE request for an <see cref="IEntity"/>.
/// It also requires a <see cref="Uuid"/> to send and provides validation.
/// </summary>
/// <typeparam name="T">The type of the result object.</typeparam>
public abstract record DeleteCommand : Command<None>, IValid
{
    public Uuid Id { get; }

    protected DeleteCommand(Uuid id)
    {
        Id = id;
    }

    protected DeleteCommand(IEntity model)
    {
        Id = model.Id;
    }

    public static IValidator Validator { get; } = new InlineValidator<DeleteCommand>
    {
        static v => v.RuleFor(x => x.Id).NotEmpty()
    };
}
