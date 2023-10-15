﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core;

public static class IWebModuleMixins
{
    public static void AddWebModule<TWebModule>(this WebApplicationBuilder builder, Action<TWebModule>? configure = null)
        where TWebModule : class, IWebModule<TWebModule>, new()
    {
        var count = builder.Services.Count;
        var module = new TWebModule();
        builder.Services.TryAddSingleton(module);

        if (count == builder.Services.Count) return;

        configure?.Invoke(module);
        TWebModule.Add(builder, module);

        Log.ForContext<TWebModule>().Debug("Added WebModule: {Module}", module.GetType().Name);
    }

    public static void UseWebModule<TWebModule>(this WebApplication app)
        where TWebModule : class, IWebModule<TWebModule>, new()
    {
        TWebModule.Use(app);
        Log.ForContext<TWebModule>().Debug("Using WebModule: {Module}", typeof(TWebModule).Name);
    }
}
