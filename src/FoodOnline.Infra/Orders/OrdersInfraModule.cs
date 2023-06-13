using FoodOnline.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Orders;

public sealed class OrdersInfraModule : IInfraModule
{
    public static void Add(IServiceCollection services, IConfiguration configuration)
    {
    }
}
