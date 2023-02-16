using FoodOnline.Models;

namespace FoodOnline.Requests;

public abstract record Upsert<TModel> : ICommand<TModel>
    where TModel : class, IModel
{
    public TModel Model { get; }

    protected Upsert(TModel model)
    {
        Model = model;
    }
}
