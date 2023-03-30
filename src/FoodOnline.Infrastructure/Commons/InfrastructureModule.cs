using FoodOnline.Abstractions;
using FoodOnline.Abstractions.Behaviors;
using FoodOnline.Authentications.BsonSerializesrs;
using FoodOnline.Commons.BsonSerializesrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnline;

public sealed class InfrastructureModule : IInfrastructureModule
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediator(static o => o.ServiceLifetime = ServiceLifetime.Singleton);

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LogBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(RunnerBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        AuthTypeSerializer.TryRegister();
        UuidBsonSerializer.TryRegister();
        RoleBsonSerializer.TryRegister();

    }
}
