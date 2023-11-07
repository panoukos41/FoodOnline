using Core;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;

namespace System.Text.Json;

public static class JsonObjectMixins
{
    public static JsonObject CopyFrom(this JsonObject obj, JsonObject other)
    {
        foreach (var (key, node) in other)
        {
            obj[key] = node;
        }
        return obj;
    }

    [return: NotNullIfNotNull(nameof(obj))]
    public static JsonObject? Upsert<T>(this JsonObject? obj, string key, T? value)
    {
        if (obj is null) return obj;

        if (value is null)
            obj.Remove(key);
        else
            obj[key] = JsonSerializer.SerializeToNode(value, Options.Json);

        return obj;
    }
}
