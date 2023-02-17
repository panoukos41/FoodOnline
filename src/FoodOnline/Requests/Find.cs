using FoodOnline.Models;

namespace FoodOnline.Requests;

public abstract record Find<TModel> : IRequest<TModel>
    where TModel : class, IModel
{
    public Uuid Id { get; }

    protected Find(Uuid id)
    {
        Id = id;
    }
}
