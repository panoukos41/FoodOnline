namespace FoodOnline.Abstractions.Requests;

/// <summary>
/// Represents a GET request that will query for some values.
/// </summary>
/// <typeparam name="T">The type of the result object.</typeparam>
public abstract record Query<T> : IQuery<Result<T>> where T : notnull
{
}

