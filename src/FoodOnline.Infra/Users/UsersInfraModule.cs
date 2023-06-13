using FoodOnline.Abstractions;
using FoodOnline.Users.BsonSerializesrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.Users;

public sealed class UsersInfraModule : IInfraModule
{
    public static void Add(IServiceCollection services, IConfiguration configuration)
    {
        RoleBsonSerializer.TryRegister();
    }
}
