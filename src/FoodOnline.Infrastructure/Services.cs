using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Infrastructure;

public static class Services
{
    private static IServiceProvider? Provider { get; set; }

    public static T? Get<T>()
    {
        Provider.CheckNotNull();
        return Provider.GetService<T>();
    }

    public static IEnumerable<T> GetMany<T>()
    {
        Provider.CheckNotNull();
        return Provider.GetServices<T>();
    }

    public static T GetRequired<T>() where T : notnull
    {
        Provider.CheckNotNull();
        return Provider.GetRequiredService<T>();
    }

    internal static void Initialize(IServiceProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);
        Provider = provider;
    }

    private static void CheckNotNull([NotNull] this IServiceProvider? provider)
    {
        if (provider is { }) return;

        throw new ArgumentNullException("Services", "No service provider was initialized. Please call Initialzie");
    }
}
