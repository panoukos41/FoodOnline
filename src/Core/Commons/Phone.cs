using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Core.Commons;

//public sealed record Phone : ISpanParsable<Phone>
public struct Phone : ISpanParsable<Phone>
{
    private static readonly SearchValues<char> separatorSearch = SearchValues.Create([' ']);
    private string? _formatted;

    public static Phone Empty { get; } = new(string.Empty, string.Empty);

    public required string CallingCode { get; init; }

    public required string Number { get; init; }

    public string Formatted => _formatted ??= $"{CallingCode} {Number}";

    [SetsRequiredMembers]
    public Phone(string callingCode, string number)
    {
        CallingCode = callingCode;
        Number = number;
    }

    public override string ToString()
    {
        return Formatted;
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Phone result)
    {
        Unsafe.SkipInit(out result);
        if (!s.StartsWith(['+'])) return false;

        var separatorIndex = s.IndexOfAny(separatorSearch);
        if (separatorIndex == -1) return false;

        result = new Phone
        {
            CallingCode = s[..(separatorIndex)].ToString(),
            Number = s[(separatorIndex + 1)..].ToString()
        };
        return true;
    }

    #region ISpanParsable Forwards

    public static Phone Parse(string s, IFormatProvider? provider = null) => Parse(s.AsSpan(), provider);

    public static Phone Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        return TryParse(s, provider, out var result)
            ? result
            : throw new InvalidOperationException("The provided phone number could not be parsed.");
    }

    public static bool TryParse(ReadOnlySpan<char> s, [MaybeNullWhen(false)] out Phone result) => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Phone result) => TryParse(s.AsSpan(), null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Phone result) => TryParse(s.AsSpan(), null, out result);

    #endregion
}
