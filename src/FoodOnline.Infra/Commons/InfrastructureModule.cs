using FoodOnline.Abstractions;
using FoodOnline.Abstractions.Behaviors;
using FoodOnline.Commons.BsonSerializesrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline;

public sealed class InfraModule : IInfraModule
{
    public static bool Configured { get; private set; }

    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        Configured = true;

        services.AddMediator(static o => o.ServiceLifetime = ServiceLifetime.Singleton);

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LogBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(RunnerBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        UuidBsonSerializer.TryRegister();
    }
}
