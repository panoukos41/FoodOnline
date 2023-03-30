using Dunet;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoodOnline;

public sealed record Er
{
    public required string Error { get; init; }

    public required string Reason { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, JsonDocument>? Metadata { get; set; }

    public bool Equals(Er? other)
    {
        return Error == other?.Error;
    }

    public override int GetHashCode()
    {
        return Error.GetHashCode();
    }
}

[Union, JsonConverter(typeof(ResultJsonConverter))]
public abstract partial record Result<T> where T : notnull
{
    public static implicit operator Result<T>(T Value) => new Ok(Value);

    public static implicit operator Result<T>(Exception ex) => new Er(ex);

    public static implicit operator Result<T>(FoodOnline.Er er) => new Er(er.Error, er.Reason, er.Metadata);

    public partial record Ok(T Value);

    public partial record Er
    {
        public string Error { get; set; }

        public string Reason { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, JsonDocument>? Metadata { get; set; }

        public Er(Exception ex)
        {
            Error = ex.GetType().Name;
            Reason = ex.Message;
        }

        [JsonConstructor]
        public Er(string error, string reason, Dictionary<string, JsonDocument>? metadata = null)
        {
            Error = error;
            Reason = reason;
            Metadata = metadata;
        }
    }

    public bool IsOk() => this is Result<T>.Ok;

    public bool IsOk([NotNullWhen(true)] out T? ok)
    {
        ok = default;
        if (this is Result<T>.Ok _ok)
        {
            ok = _ok.Value;
            return true;
        }
        return false;
    }

    public bool IsOk([NotNullWhen(true)] out T? ok, [NotNullWhen(false)] out Er? er)
    {
        Unsafe.SkipInit(out er);
        if (IsOk(out ok)) return true;

        er = (Result<T>.Er)this;
        return false;
    }

    public bool IsEr() => this is Result<T>.Er;

    public bool IsEr([NotNullWhen(true)] out Result<T>.Er? er)
    {
        er = default;
        if (this is Result<T>.Er _er)
        {
            er = _er;
            return true;
        }
        return false;
    }

    public bool IsEr([NotNullWhen(true)] out Result<T>.Er? er, [NotNullWhen(false)] out Result<T>.Ok? ok)
    {
        Unsafe.SkipInit(out ok);
        if (IsEr(out er)) return true;

        ok = (Result<T>.Ok)this;
        return false;
    }

    public virtual bool Equals(Result<T>? other)
    {
        if (this is Result<T>.Ok ok &&
            other is Result<T>.Ok okOther)
        {
            return EqualityComparer<T>.Default.Equals(ok.Value, okOther.Value);
        }

        var er = (Result<T>.Er)this;
        var erOther = other as Result<T>.Er;

        return er.Error == erOther?.Error;
    }

    public virtual bool Equals(FoodOnline.Er? other)
    {
        return IsEr(out var er) && er.Error == other?.Error;
    }

    public override int GetHashCode()
    {
        return Match(
            static ok => ok.Value.GetHashCode(),
            static er => er.Error.GetHashCode());
    }
}

public sealed class ResultJsonConverter : JsonConverterFactory
{
    private static readonly Type ResultType = typeof(Result<>);
    private static readonly Type converterType = typeof(Converter<>);

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsGenericType
            && typeToConvert.GetGenericTypeDefinition() == ResultType;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var type = typeToConvert.GetGenericArguments()[0];
        var converter = converterType.MakeGenericType(type);

        return (JsonConverter)Activator.CreateInstance(converter)!;
    }

    private class Converter<T> : JsonConverter<Result<T>>
        where T : notnull
    {
        private static readonly JsonEncodedText Result = JsonEncodedText.Encode("$result");
        private static readonly JsonEncodedText Value = JsonEncodedText.Encode("$value");
        private static readonly JsonEncodedText Ok = JsonEncodedText.Encode("ok");
        private static readonly JsonEncodedText Er = JsonEncodedText.Encode("er");

        public override Result<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var readerAtStart = reader;
            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName ||
                !reader.ValueTextEquals(Result.EncodedUtf8Bytes))
            {
                throw new JsonException("Expected the first value to be a property named $result");
            }

            reader.Read();
            Result<T>? result;
            if (reader.ValueTextEquals(Ok.EncodedUtf8Bytes))
            {
                reader.Read();
                if (reader.ValueTextEquals(Value.EncodedUtf8Bytes))
                {
                    reader.Read();
                    var ok = JsonSerializer.Deserialize<T>(ref reader, options);
                    result = ok is { } ? ok
                        : new JsonException("Could not deserialize 'OK' $value because it was null.");
                }
                else
                {
                    var ok = JsonSerializer.Deserialize<T>(ref readerAtStart, options);
                    result = ok is { } ? ok
                        : new JsonException("Could not deserialize 'OK' object because it was null.");
                }
            }
            else if (reader.ValueTextEquals(Er.EncodedUtf8Bytes))
            {
                var er = JsonSerializer.Deserialize<Result<T>.Er>(ref readerAtStart, options);
                result = er is { } ? er
                    : new JsonException("Could not deserialize 'OK' object because it was null.");
            }
            else
            {
                throw new JsonException();
            }
            while (reader.Read()) { }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, Result<T> value, JsonSerializerOptions options)
        {
            var ok = value is Result<T>.Ok;
            var e = ok
                ? JsonSerializer.SerializeToElement(((Result<T>.Ok)value).Value, options)
                : JsonSerializer.SerializeToElement((Result<T>.Er)value, options);

            writer.WriteStartObject();
            writer.WriteString(Result, ok ? Ok : Er);
            if (e.ValueKind is JsonValueKind.Object or JsonValueKind.Array)
            {
                foreach (var obj in e.EnumerateObject())
                {
                    obj.WriteTo(writer);
                }
            }
            else
            {
                writer.WritePropertyName(Value);
                e.WriteTo(writer);
            }
            writer.WriteEndObject();
        }
    }
}
