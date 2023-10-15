using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Orders;

public sealed class OrdersAppModule : IAppModule<OrdersAppModule>
{
    public string CollectionName { get; set; } = "orders";

    public static void Add(IServiceCollection services, IConfiguration configuration, OrdersAppModule module)
    {
    }
}
