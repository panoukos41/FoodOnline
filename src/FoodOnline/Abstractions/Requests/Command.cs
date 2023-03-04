using FluentValidation;
using FoodOnline.Models;
using FoodOnline.Validation;

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
/// also requires an <see cref="IModel"/> to send and provides validation.
/// </summary>
/// <typeparam name="T">The type of the result object.</typeparam>
public abstract record SetCommand<TModel> : Command<TModel>, IValid where TModel : notnull, IModel
{
    public TModel Model { get; }

    protected SetCommand(TModel model)
    {
        Model = model;
    }

    public static IValidator Validator { get; } = new InlineValidator<SetCommand<TModel>>
    {
        static v => v.RuleFor(x => x.Model).SetValidator((IValidator<TModel>)TModel.Validator)
    };
}

/// <summary>
/// Represents a DELETE request for an <see cref="IModel"/>.
/// It also requires a <see cref="Uuid"/> to send and provides validation.
/// </summary>
/// <typeparam name="T">The type of the result object.</typeparam>
public abstract record DeleteCommand : Command<Unit>, IValid
{
    public Uuid Id { get; }

    protected DeleteCommand(Uuid id)
    {
        Id = id;
    }

    protected DeleteCommand(IModel model)
    {
        Id = model.Id;
    }

    public static IValidator Validator { get; } = new InlineValidator<DeleteCommand>
    {
        static v => v.RuleFor(x => x.Id).NotEmpty()
    };
}
