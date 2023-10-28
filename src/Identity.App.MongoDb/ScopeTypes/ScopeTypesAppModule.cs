using Identity.ScopeTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace Identity.MongoDb.ScopeTypes;

public sealed class ScopeTypesAppModule : IAppModule<ScopeTypesAppModule>
{
    public string CollectionName { get; set; } = "scope";

    public static void Add(IServiceCollection services, IConfiguration configuration, ScopeTypesAppModule module)
    {
        BsonClassMap.RegisterClassMap<ScopeType>(map =>
        {
            map.AutoMap();
            map.MapIdField(x => x.Id);
        });

        services.AddSingleton<OpenIddictScopeTypeStore>();
    }
}
