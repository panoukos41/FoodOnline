using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Abstractions;

public interface IInfrastructureModule
{
    public abstract static void Configure(IServiceCollection services, IConfiguration configuration);
}
