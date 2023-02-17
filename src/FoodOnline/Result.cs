using Dunet;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;

namespace FoodOnline;

public interface IResult
{
}

[Union]
public partial record Result<T> : IResult
{
    public static implicit operator Result<T>(T Value) => new Ok(Value);

    public static implicit operator Result<T>(Exception ex) => new Err(ex);

    public partial record Ok(T Value);

    public partial record Err
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        [JsonPropertyName("metadata"), JsonExtensionData]
        public IDictionary<string, string>? Metadata { get; set; }

        public Err(Exception ex)
        {
            Error = ex.GetType().Name;
            Reason = ex.Message;
        }
    }
}

public static class ResultMixins
{
    public static T? TryGetValue<T>(this Result<T> result)
        => result.Match<T?>(static ok => ok.Value, static err => default);

    public static bool TryGetValue<T>(this Result<T> result, [NotNullWhen(true), MaybeNullWhen(false)] out T? value)
    {
        value = TryGetValue(result);
        return value is { };
    }
}
