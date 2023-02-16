using FoodOnline.Models;

namespace FoodOnline.Requests;

public abstract record Remove<TModel> : ICommand
    where TModel : class, IModel
{
    public string Id { get; }

    protected Remove(string id)
    {
        Id = id;
    }

    protected Remove(TModel model)
    {
        Id = model.Id;
    }
}
