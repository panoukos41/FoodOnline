using FoodOnline.Models;

namespace FoodOnline.Requests;

public abstract record Remove<TModel> : ICommand
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
}
