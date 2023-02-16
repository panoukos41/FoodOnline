using FoodOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnline.Requests.UserRequests;

public sealed record UpsertUser : Upsert<UserModel>
{
    public UpsertUser(UserModel model) : base(model)
    {
    }
}
