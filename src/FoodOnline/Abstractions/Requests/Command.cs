using FluentValidation;

namespace FoodOnline.Abstractions.Requests;

/// <summary>
/// Represents a base command.
/// </summary>
/// <typeparam name="T">The type of the result object.</typeparam>
public abstract record Command<T> : ICommand<Result<T>>
    where T : notnull
{
}

/// <summary>
/// Represents a POST request that will Insert
/// a new <see cref="IEntity"/> that must be valid.
/// The returned value is the <see cref="Uuid"/>
/// the inserted entity got.
/// </summary>
/// <typeparam name="TEntity">The type of the entity to insert.</typeparam>
public abstract record InsertCommand<TEntity> : Command<Uuid>, IValid
    where TEntity : notnull, IEntity
{
    public TEntity Entity { get; }

    protected InsertCommand(TEntity entity)
    {
        Entity = entity;
    }

    public static IValidator Validator { get; } = new InlineValidator<InsertCommand<TEntity>>
    {
        static v => v.RuleFor(x => x.Entity).SetValidator((IValidator<TEntity>)TEntity.Validator)
    };
}

/// <summary>
/// Represents a PUT request that will Update
/// an existing <see cref="IEntity"/> that must be valid.
/// </summary>
/// <typeparam name="TEntity">The type of the entity to update.</typeparam>
public abstract record UpdateCommand<TEntity> : Command<None>, IValid
    where TEntity : notnull, IEntity
{
    public TEntity Entity { get; }

    protected UpdateCommand(TEntity entity)
    {
        Entity = entity;
    }

    public static IValidator Validator { get; } = new InlineValidator<UpdateCommand<TEntity>>
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
