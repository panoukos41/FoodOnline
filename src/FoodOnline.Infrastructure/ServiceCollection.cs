global using Mediator;
using FoodOnline.Infrastructure.Behaviors;
using FoodOnline.Infrastructure.Serializesrs.Bson;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Infrastructure;

public static class ServiceCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediator(static o => o.ServiceLifetime = ServiceLifetime.Singleton);

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LogBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        UuidBsonSerializer.TryRegister();
        RoleBsonSerializer.TryRegister();

        return services;
    }
}
