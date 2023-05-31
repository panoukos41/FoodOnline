using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Users;

public sealed class Claims : List<Claim>, IEquatable<Claims>
{
    public Claims()
    {
    }

    public Claims(IEnumerable<Claim> collection) : base(collection)
    {
    }

    public Claims(int capacity) : base(capacity)
    {
    }

    public override string ToString()
    {
        return string.Join(", ", this.Select(static c => c.ToString()));
    }

    public override int GetHashCode()
    {
        return this.Aggregate(Count.GetHashCode(), (value, next) => HashCode.Combine(value, next));
    }

    public override bool Equals(object? obj)
    {
        return obj is Claims claims && Equals(claims);
    }

    public bool Equals(Claims? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return this.SequenceEqual(other);
    }
}

public sealed record Claim
{
    public required string Type { get; set; }

    public required string Value { get; set; }

    public Claim()
    {
    }

    [SetsRequiredMembers]
    public Claim(string type, string value)
    {
        Type = type;
        Value = value;
    }

    public override string ToString() => $$"""
        {"{{Type}}":"{{Value}}"}
        """;
}
