namespace FoodOnline.Users.Requests;

public sealed record CreateUser : PostCommand<User>
{
    public CreateUser(User entity) : base(entity)
    {
    }
}
