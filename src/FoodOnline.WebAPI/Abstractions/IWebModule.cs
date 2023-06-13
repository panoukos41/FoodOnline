namespace FoodOnline.Abstractions;

public interface IWebModule
{
    abstract static void Add(WebApplicationBuilder builder);

    abstract static void Use(WebApplication app);
}
