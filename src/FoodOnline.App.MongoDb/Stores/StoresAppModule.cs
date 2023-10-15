using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Stores;

public sealed class StoresAppModule : IAppModule<StoresAppModule>
{
    public string CollectionName { get; set; } = "stores";

    public static void Add(IServiceCollection services, IConfiguration configuration, StoresAppModule module)
    {
    }
}
