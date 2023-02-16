using FoodOnline.Abstractions;

namespace FoodOnline.Common;

public sealed class Strings : SetBase<Strings, string>
{
    public Strings()
    {
    }

    public Strings(int capacity) : base(capacity)
    {
    }

    public Strings(IEnumerable<string> collection) : base(collection)
    {
    }

    protected override Func<string, string> ParseItem => static v => v;
}
