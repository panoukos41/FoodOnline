using FoodOnline.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnline.Users;

public sealed class UsersModule : IInfrastructureModule
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
    }
}
