using FluentValidation;
using FoodOnline.Validation;

namespace FoodOnline.Abstractions.Requests;

/// <summary>
/// Represents a GET request.
/// </summary>
/// <typeparam name="T">The type of the result object.</typeparam>
public abstract record Request<T> : IRequest<Result<T>> where T : notnull
{
}

/// <summary>
/// Represents a GET request like <see cref="Request{T}"/> but
/// also requires a <see cref="Uuid"/> to send and provides validation.
/// </summary>
/// <typeparam name="T">The type of the result object.</typeparam>
public abstract record GetRequest<T> : Request<T>, IValid where T : notnull, IId
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
