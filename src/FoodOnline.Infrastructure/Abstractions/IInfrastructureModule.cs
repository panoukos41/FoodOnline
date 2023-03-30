using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Abstractions;

public interface IInfrastructureModule
{
    abstract static void Configure(IServiceCollection services, IConfiguration configuration);
}
