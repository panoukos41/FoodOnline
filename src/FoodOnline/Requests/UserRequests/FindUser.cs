using FoodOnline.Models;

namespace FoodOnline.Requests.UserRequests;

public sealed record FindUser : Find<UserModel>
{
    public FindUser(string id) : base(id)
    {
    }
}
