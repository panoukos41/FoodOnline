using FluentValidation;

namespace Core.Abstractions.Requests;

/// <summary>
/// Represents a query request.
/// </summary>
/// <typeparam name="TResult">The type of the result object.</typeparam>
public abstract record Query<TResult> : IQuery<Result<TResult>> where TResult : notnull
{
}

/// <summary>
/// Represents a <see cref="Query{TResult}"/> that will search by Id.
/// </summary>
/// <typeparam name="TResult">The type of the result object.</typeparam>
public abstract record FindQuery<TResult> : Query<TResult>, IValid where TResult : notnull
{
    public Uuid Id { get; }

    protected FindQuery(Uuid id)
    {
        Id = id;
    }

    public static IValidator Validator { get; } = new InlineValidator<FindQuery<TResult>>
    {
        static v => v.RuleFor(x => x.Id).NotEmpty()
    };
}
