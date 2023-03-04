global using Mediator;
using FoodOnline.Infrastructure;
using FoodOnline.Infrastructure.Behaviors;
using FoodOnline.Infrastructure.Serializesrs.Bson;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceMixins
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediator(static o => o.ServiceLifetime = ServiceLifetime.Singleton);

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LogBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(RunnerBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        UuidBsonSerializer.TryRegister();
        RoleBsonSerializer.TryRegister();

        return services;
    }

    public static IHost UseInfrastructureServiceProvider(this IHost app)
    {
        Services.Initialize(app.Services);

        return app;
    }
}
