using FoodOnline.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Stores;

public sealed class StoresModule : IInfraModule
{
    public static bool Configured { get; private set; }

    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        Configured = true;
    }
}
