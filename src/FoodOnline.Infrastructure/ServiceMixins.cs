global using Mediator;
using FoodOnline;
using FoodOnline.Abstractions;
using FoodOnline.Abstractions.Behaviors;
using FoodOnline.Authentications.BsonSerializesrs;
using FoodOnline.Commons.BsonSerializesrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceMixins
{
    public static void AddInfrastructureModule<TModule>(this IServiceCollection services, IConfiguration configuration)
        where TModule : IInfrastructureModule
    {
        TModule.Configure(services, configuration);
    }

    public static void AddAllInfrastructureModules(this IServiceCollection services, IConfiguration configuration)
    {
        var args = new object[] { services, configuration };

        //var moduleType = typeof(IInfrastructureModule);
        //var methodType = moduleType.GetMethod(nameof(IInfrastructureModule.Configure), BindingFlags.Public | BindingFlags.Static);


        var types = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(static assembly => assembly.DefinedTypes)
            .Where(static t => t.GetInterface(nameof(IInfrastructureModule)) is { })
            .ToList();

        //foreach (var type in types)
        //{
        //    var method = type.GetMethod(nameof(IInfrastructureModule.Configure), BindingFlags.Public | BindingFlags.Static);
        //    method?.Invoke(null, args);
        //}
    }
}
