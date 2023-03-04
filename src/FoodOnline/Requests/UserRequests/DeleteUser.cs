using FoodOnline.Abstractions.Requests;
using FoodOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
