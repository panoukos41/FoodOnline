namespace FoodOnline.Users.Requests;

public sealed record GetUser : FindQuery<User>
{
    public GetUser(Uuid id) : base(id)
    {
    }
}
