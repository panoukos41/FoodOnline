﻿using FoodOnline.Abstractions;

namespace FoodOnline.Orders;

/// <summary>
/// This already adds <see cref="OrdersInfraModule"/>
/// </summary>
public sealed class OrdersWebModule : IWebModule
{
    public static void Add(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.Add<OrdersInfraModule>(configuration);
    }

    public static void Use(WebApplication app)
    {
        var group = app.MapGroup("orders");
    }
}
