using FoodOnline.Abstractions;

namespace FoodOnline.Common;

public sealed class Ints : SetBase<Ints, int>
{
    public Ints()
    {
    }

    public Ints(int capacity) : base(capacity)
    {
    }

    public Ints(IEnumerable<int> collection) : base(collection)
    {
    }

    protected override Func<string, int> ParseItem => int.Parse;
}
