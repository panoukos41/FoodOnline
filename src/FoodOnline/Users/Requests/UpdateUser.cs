namespace FoodOnline.Users.Requests;

public sealed record UpdateUser : UpdateCommand<User>
{
    public UpdateUser(User entity) : base(entity)
    {
    }
}
