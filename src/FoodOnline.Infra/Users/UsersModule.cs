using FoodOnline.Abstractions;
using FoodOnline.Users.BsonSerializesrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Users;

public sealed class UsersModule : IInfraModule
{
    public static bool Configured { get; private set; }

    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        Configured = true;

        RoleBsonSerializer.TryRegister();
    }
}
