namespace FoodOnline.Users.Requests;

public sealed record GetUser : GetQuery<User>
{
    public GetUser(Uuid id) : base(id)
    {
    }
}
