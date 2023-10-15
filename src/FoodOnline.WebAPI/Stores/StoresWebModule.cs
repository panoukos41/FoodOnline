using FoodOnline.Stores.Requests;
using Mediator;

namespace FoodOnline.Stores;

/// <summary>
/// This already adds <see cref="StoresAppModule"/>
/// </summary>
public sealed class StoresWebModule : IWebModule<StoresWebModule>
{
    public static void Add(WebApplicationBuilder builder, StoresWebModule module)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddAppModule<StoresAppModule>(configuration);
    }

    public static void Use(WebApplication app)
    {
        var group = app.MapGroup("stores");
        group.WithTags("Stores");

        group.MapGet("{storeId}", (string storeId, ISender sender) =>
        {
            //return sender.Send(new GetStore(Uuid.Parse(storeId))).Ok();
        });

        group.MapPost("", (CreateStore createStore, ISender sender) =>
        {
            //return sender.Send(createStore).Ok();
        });

        group.MapPut("", (UpdateStore updateStore, ISender sender) =>
        {
            //return sender.Send(updateStore).Ok();
        });
    }
}
