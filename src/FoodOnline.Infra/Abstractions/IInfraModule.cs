using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Abstractions;

public interface IInfraModule
{
    abstract static void Configure(IServiceCollection services, IConfiguration configuration);
}
