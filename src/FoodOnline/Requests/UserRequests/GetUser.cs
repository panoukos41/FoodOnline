using FoodOnline.Abstractions.Requests;
using FoodOnline.Models;

namespace FoodOnline.Requests.UserRequests;

public sealed record GetUser : GetRequest<UserModel>
{
    public GetUser(Uuid id) : base(id)
    {
    }
}
