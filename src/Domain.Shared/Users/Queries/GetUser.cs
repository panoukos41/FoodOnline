using FoodOnline.Domain.Users.Models;
using MediatR;

namespace FoodOnline.Domain.Users.Queries
{
    public class GetUser : IRequest<UserModel>
    {
        public string Id { get; set; }
    }
}