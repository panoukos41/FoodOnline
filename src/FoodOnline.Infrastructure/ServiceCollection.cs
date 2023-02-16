global using Mediator;
using FoodOnline.Common;
using FoodOnline.Infrastructure.Behaviors;
using FoodOnline.Infrastructure.Serializesrs.Bson;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace FoodOnline.Infrastructure;

public static class ServiceCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediator(static o => o.ServiceLifetime = ServiceLifetime.Singleton);

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LogBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

    [ModuleInitializer]
    [SuppressMessage("Usage", "CA2255", Justification = "<Pending>")]
    public static void Init()
    {

#pragma warning disable CS0618 // Type or member is obsolete
        //BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
        //BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
#pragma warning restore CS0618 // Type or member is obsolete

        BsonSerializer.RegisterSerializer(new RoleBsonSerializer());
    }
}
