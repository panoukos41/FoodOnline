namespace FoodOnline.Stores.Requests;

public sealed record SetStoreOpen : Command<None>
{
    public Uuid Id { get; }

    public bool Open { get;}

    public SetStoreOpen(Uuid id, bool open)
    {
        Id = id;
        Open = open;
    }
}
