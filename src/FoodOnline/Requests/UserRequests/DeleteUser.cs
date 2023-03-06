using FoodOnline.Abstractions.Requests;
using FoodOnline.Models;

namespace FoodOnline.Requests.UserRequests;

public sealed record DeleteUser : DeleteCommand
{
    public DeleteUser(Uuid id) : base(id)
    {
    }

    public DeleteUser(UserModel model) : base(model)
    {
    }
}
