using FoodOnline.Abstractions;
using FoodOnline.Commons.Behaviors;
using FoodOnline.Commons.BsonSerializesrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace FoodOnline;

public sealed class CommonInfraModule : IInfraModule
{
    public static void Add(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediator(static o => o.ServiceLifetime = ServiceLifetime.Singleton);

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LogBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(RunnerBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        UuidBsonSerializer.TryRegister();

        services.AddSingleton(sp =>
        {
            var mongo = sp.GetRequiredService<IConfiguration>().GetConnectionString("mongo");
            var db = sp.GetRequiredService<IConfiguration>().GetConnectionString("db");
            var client = new MongoClient(mongo ?? "mongodb://admin:password@localhost:3306");
            return client.GetDatabase(db ?? "food-online");
        });
    }
}
