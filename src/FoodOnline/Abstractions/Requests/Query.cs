using FluentValidation;

namespace FoodOnline.Abstractions.Requests;

/// <summary>
/// Represents a GET request that will query for some values.
/// </summary>
/// <typeparam name="T">The type of the result object.</typeparam>
public abstract record Query<T> : IQuery<Result<T>> where T : notnull
{
}

/// <summary>
/// Represents a GET request that also requires
/// a <see cref="Uuid"/> to send and provides validation for.
/// </summary>
/// <typeparam name="T">The type of the result object.</typeparam>
public abstract record GetQuery<T> : Query<T>, IValid where T : notnull
{
    public Uuid Id { get; }

    protected GetQuery(Uuid id)
    {
        Id = id;
    }

    public static IValidator Validator { get; } = new InlineValidator<GetQuery<T>>
    {
        static v => v.RuleFor(x => x.Id).NotEmpty(),
    };
}
