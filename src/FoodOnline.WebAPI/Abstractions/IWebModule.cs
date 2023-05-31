namespace FoodOnline.Abstractions;

public interface IWebModule
{
    abstract static void Configure(WebApplicationBuilder builder);

    abstract static void Use(WebApplication app);
}
