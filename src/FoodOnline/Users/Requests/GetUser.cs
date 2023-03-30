namespace FoodOnline.Users.Requests;

public sealed record GetUser : GetRequest<User>
{
    public GetUser(Uuid id) : base(id)
    {
    }
}
