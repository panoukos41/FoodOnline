namespace FoodOnline.Users.Requests;

public sealed record UpdateUser : PutCommand<User>
{
    public UpdateUser(User entity) : base(entity)
    {
    }
}
