using FoodOnline.Orders.Requests;
using Mediator;

namespace FoodOnline.Orders;

/// <summary>
/// This already adds <see cref="OrdersAppModule"/>
/// </summary>
public sealed class OrdersWebModule : IWebModule<OrdersWebModule>
{
    public static void Add(WebApplicationBuilder builder, OrdersWebModule module)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddAppModule<OrdersAppModule>(configuration);
    }

    public static void Use(WebApplication app)
    {
        var group = app.MapGroup("orders");
        group.WithTags("Orders");

        group.MapGet("{orderId}", (string orderId, ISender sender) =>
        {
            //return sender.Send(new GetOrder(Uuid.Parse(orderId))).Ok();
        });

        group.MapPost("", (PlaceOrder createOrder, ISender sender) =>
        {
            //return sender.Send(createOrder).Ok();
        });

        group.MapPut("", (UpdateOrder updateOrder, ISender sender) =>
        {
            //return sender.Send(updateOrder).Ok();
        });
    }
}
