using FoodOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnline.Requests.UserRequests;

public sealed record RemoveUser : Remove<UserModel>
{
    public RemoveUser(string id) : base(id)
    {
    }

    public RemoveUser(UserModel model) : base(model)
    {
    }
}
