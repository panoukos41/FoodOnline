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
/// Represents a GET request like that also requires
/// a <see cref="Uuid"/> to send and provides validation.
/// </summary>
/// <typeparam name="T">The type of the result object.</typeparam>
public abstract record GetRequest<T> : Query<T>, IValid where T : notnull
{
    public Uuid Id { get; }

    protected GetRequest(Uuid id)
    {
        Id = id;
    }

    public static IValidator Validator { get; } = new InlineValidator<GetRequest<T>>
    {
        static v => v.RuleFor(x => x.Id).NotEmpty(),
    };
}
