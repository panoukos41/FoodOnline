using FoodOnline.Models;

namespace FoodOnline.Requests;

public abstract record Find<TModel> : IRequest<TModel>
    where TModel : class, IModel
{
    public string Id { get; }

    protected Find(string id)
    {
        Id = id;
    }
}
