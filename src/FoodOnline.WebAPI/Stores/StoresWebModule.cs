﻿using FoodOnline.Abstractions;

namespace FoodOnline.Stores;

public sealed class StoresWebModule : IWebModule
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
