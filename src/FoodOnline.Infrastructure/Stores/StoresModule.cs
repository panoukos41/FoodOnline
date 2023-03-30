using FoodOnline.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnline.Stores;

public sealed class StoresModule : IInfrastructureModule
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
    }
}
