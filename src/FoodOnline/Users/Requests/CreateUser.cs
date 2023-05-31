namespace FoodOnline.Users.Requests;

public sealed record CreateUser : CreateCommand<User>
{
    public CreateUser(User entity) : base(entity)
    {
    }
}
