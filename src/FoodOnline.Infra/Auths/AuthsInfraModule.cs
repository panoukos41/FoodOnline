using FoodOnline.Abstractions;
using FoodOnline.Auths.BsonSerializesrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Auths;

public sealed class AuthsInfraModule : IInfraModule
{
    public static void Add(IServiceCollection services, IConfiguration configuration)
    {
        AuthTypeSerializer.TryRegister();
    }
}
