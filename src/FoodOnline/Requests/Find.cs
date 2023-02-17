using FluentValidation;
using FoodOnline.Abstractions;
using FoodOnline.Models;
using FoodOnline.Tools;

namespace FoodOnline.Requests;

public abstract record Find<TModel> : IRequest<Result<TModel>>, IValid<Find<TModel>>
    where TModel : class, IModel
{
    public Uuid Id { get; }

    protected Find(Uuid id)
    {
        Id = id;
    }

    public static IValidator<Find<TModel>> Validator { get; } =
        new FlValidator<Find<TModel>>(v =>
        {
            v.RuleFor(x => x.Id).NotEmpty();
        });

    static IValidator IValid.Validator => Validator;
}
