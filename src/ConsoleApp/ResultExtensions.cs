namespace FoodOnline;

public static class ResultExtensions
{
    /// <summary>
    /// Produces a <see cref="StatusCodes.Status200OK"/> response.
    /// </summary>
    public static ValueTask<IResult> Ok<T>(this ValueTask<Result<T>> result)
        where T : notnull
        => result.MatchAsync(Results.Ok, Error);

    /// <summary>
    /// Produces a <see cref="StatusCodes.Status201Created"/> response.
    /// </summary>
    /// <param name="createdAt">Produces the URI at which the content has been created.</param>
    public static ValueTask<IResult> Created<T>(this ValueTask<Result<T>> result, Func<T, string> createdAt)
        where T : notnull
        => result.MatchAsync(ok => Results.Created(createdAt(ok.Value), ok), Error);

    /// <summary>
    /// Produces a <see cref="StatusCodes.Status202Accepted"/> response.
    /// </summary>
    /// <param name="acceptedAt">Produces the URI with the location at which the status of requested content can be monitored.</param>
    public static ValueTask<IResult> Accepted<T>(this ValueTask<Result<T>> result, Func<T, string> acceptedAt)
        where T : notnull
        => result.MatchAsync(ok => Results.Created(acceptedAt(ok.Value), ok), Error);

    /// <summary>
    /// Produces a <see cref="StatusCodes.Status204NoContent"/> response.
    /// </summary>
    public static ValueTask<IResult> NoContent<T>(this ValueTask<Result<T>> result)
        where T : notnull
        => result.MatchAsync(static ok => Results.NoContent(), Error);

    private static IResult Error<T>(Result<T>.Er er)
        where T : notnull
        => Results.BadRequest(er);

    ///// <summary>
    ///// Produces a <see cref="StatusCodes.Status400BadRequest"/> response.
    ///// </summary>
    //private static IResult BadRequest<T>();

    ///// <summary>
    ///// Produces a <see cref="StatusCodes.Status401Unauthorized"/> response.
    ///// </summary>
    //private static IResult Unauthorized<T>();

    ///// <summary>
    ///// Produces a <see cref="StatusCodes.Status404NotFound"/> response.
    ///// </summary>
    //private static IResult NotFound<T>();

    ///// <summary>
    ///// Produces a <see cref="StatusCodes.Status409Conflict"/> response.
    ///// </summary>
    //private static IResult Conflict<T>();

    ///// <summary>
    ///// Produces a <see cref="StatusCodes.Status422UnprocessableEntity"/> response.
    ///// </summary>
    //private static IResult UnprocessableEntity<T>();
}
