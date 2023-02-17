using FluentValidation;
using FoodOnline.Abstractions;
using FoodOnline.Models;
using FoodOnline.Tools;
using System.Text;

namespace FoodOnline.Requests;

public abstract record Upsert<TModel> : ICommand<Result<TModel>>, IValid<Upsert<TModel>>
    where TModel : class, IModel
{
    public TModel Model { get; }

    protected Upsert(TModel model)
    {
        Model = model;
    }

    public static IValidator<Upsert<TModel>> Validator { get; } =
        new FlValidator<Upsert<TModel>>(v =>
        {
            v.RuleFor(x => x.Model).SetValidator((IValidator<TModel>)TModel.Validator);
        });

    static IValidator IValid.Validator => Validator;
}
