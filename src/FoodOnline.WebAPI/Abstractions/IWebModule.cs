namespace FoodOnline.Abstractions;

public interface IWebModule
{
    abstract static bool Configured { get; }

    abstract static bool Used { get; }

    abstract static void Configure(WebApplicationBuilder builder);

    abstract static void Use(WebApplication app);
}
