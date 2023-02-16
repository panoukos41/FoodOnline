﻿using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Abstractions;

public abstract class SetBase<TSelf, T> : HashSet<T>
    where TSelf : SetBase<TSelf, T>, new()
    where T : notnull
{
    protected abstract Func<string, T> ParseItem { get; }

    public SetBase()
    {
    }

    public SetBase(int capacity) : base(capacity)
    {
    }

    public SetBase(IEnumerable<T> collection) : base(collection)
    {
    }

    private const char seperator = ',';

    public override string ToString()
        => string.Join(seperator, this.Select(static x => x.ToString()));

    public static TSelf Parse(string value)
    {
        var set = new TSelf();
        var items = value.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

        if (items.Length == 0) return set;

        var count = items.Length;
        for (int i = 0; i < count; i++)
        {
            set.Add(set.ParseItem(items[i]));
        }

        return set;
    }

    public override bool Equals(object? obj)
        => Equals(this as TSelf, obj as TSelf);

    public override int GetHashCode()
        => GetHashCode((TSelf)this);

    public static bool Equals(TSelf? left, TSelf? right)
        => left is { } && right is { } && left.SetEquals(right);

    public static int GetHashCode([DisallowNull] TSelf set)
        => set.Aggregate(0, static (a, b) => HashCode.Combine(a, b.GetHashCode()));

    public static implicit operator string(SetBase<TSelf, T> set) => set.ToString();
}