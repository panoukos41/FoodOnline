using FoodOnline.Abstractions;

namespace FoodOnline.Users;

public sealed class UsersWebModule : IWebModule
{
    public static bool Configured { get; private set; }

    public static bool Used { get; private set; }

    public static void Configure(WebApplicationBuilder builder)
    {
        Configured = true;
    }

    public static void Use(WebApplication app)
    {
        Used = true;
    }
}
