using FoodOnline.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Stores;

public sealed class StoresInfraModule : IInfraModule
{
    public static void Add(IServiceCollection services, IConfiguration configuration)
    {
    }
}
