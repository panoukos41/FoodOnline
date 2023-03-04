using FoodOnline.Abstractions.Requests;
using FoodOnline.Models;

namespace FoodOnline.Requests.UserRequests;

public sealed record SetUser : SetCommand<UserModel>
{
    public SetUser(UserModel model) : base(model)
    {
    }
}
