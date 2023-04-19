using FoodOnline.Abstractions;
using FoodOnline.Authentications.BsonSerializesrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Authentications;

public sealed class AuthenticationsModule : IInfraModule
{
    public static bool Configured { get; private set; }

    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        Configured = true;

        AuthTypeSerializer.TryRegister();
    }
}
