using FoodOnline.Abstractions;
using FoodOnline.Auths.BsonSerializesrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Auths;

public sealed class AuthsAppModule : IAppModule
{
    public static void Add(IServiceCollection services, IConfiguration configuration)
    {
        AuthTypeSerializer.TryRegister();
    }
}
