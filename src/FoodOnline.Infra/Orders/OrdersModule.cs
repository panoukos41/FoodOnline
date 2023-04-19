using FoodOnline.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Orders;

public sealed class OrdersModule : IInfraModule
{
    public static bool Configured { get; private set; }

    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        Configured = true;
    }
}
