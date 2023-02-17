using FluentValidation;
using FoodOnline.Abstractions;
using FoodOnline.Models;
using FoodOnline.Tools;

namespace FoodOnline.Requests;

public abstract record Remove<TModel> : ICommand<Result<Unit>>, IValid<Remove<TModel>>
    where TModel : class, IModel
{
    public Uuid Id { get; }

    protected Remove(Uuid id)
    {
        Id = id;
    }

    protected Remove(TModel model)
    {
        Id = model.Id;
    }

    public static IValidator<Remove<TModel>> Validator { get; } =
        new FlValidator<Remove<TModel>>(v =>
        {
            v.RuleFor(x => x.Id).NotEmpty();
        });

    static IValidator IValid.Validator => Validator;
}
