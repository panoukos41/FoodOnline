using FoodOnline.Abstractions;

namespace FoodOnline.Commons;

public class WebApiModule : IWebModule
{
    public static bool Configured { get; private set; }

    public static bool Used { get; private set; }

    public static void Configure(WebApplicationBuilder builder)
    {
        Configured = true;

        var services = builder.Services;

        //services.AddAuthentication();
        //services.AddAuthorization();
    }

    public static void Use(WebApplication app)
    {
        Used = true;

        //app.UseAuthentication();
        //app.UseAuthorization();

    }
}
