using Microsoft.AspNetCore.Http;

namespace Core.Commons;

public static class IResultUnionMixins
{
    /// <summary>
    /// Produces a <see cref="StatusCodes.Status200OK"/> response.
    /// </summary>
    public static ValueTask<IResult> Ok<T>(this ValueTask<Result<T>> result)
        where T : notnull
    {
        return result.MatchAsync(ok => Results.Ok(ok.Value), Error);
    }

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
        => result.MatchAsync(ok => Results.NoContent(), Error);

    private static IResult Error<T>(Result<T>.Er er)
        where T : notnull
        => er.Problem switch
        {
            { Status: 400 } => TypedResults.Unauthorized(), //"Bad Request"
            { Status: 401 } => TypedResults.Unauthorized(), //"Unauthorized"
            { Status: 403 } => TypedResults.Unauthorized(), //"Forbidden"
            { Status: 404 } => TypedResults.Unauthorized(), //"Not Found"
            { Status: 405 } => TypedResults.Unauthorized(), //"Method Not Allowed"
            { Status: 406 } => TypedResults.Unauthorized(), //"Not Acceptable"
            { Status: 408 } => TypedResults.Unauthorized(), //"Request Timeout"
            { Status: 409 } => TypedResults.Unauthorized(), //"Conflict"
            { Status: 412 } => TypedResults.Unauthorized(), //"Precondition Failed"
            { Status: 415 } => TypedResults.Unauthorized(), //"Unsupported Media Type"
            { Status: 422 } => TypedResults.Unauthorized(), //"Unprocessable Entity"
            { Status: 426 } => TypedResults.Unauthorized(), //"Upgrade Required"
            { Status: 500 } => TypedResults.Unauthorized(), //"An error occurred while processing your request."
            { Status: 502 } => TypedResults.Unauthorized(), //"Bad Gateway"
            { Status: 503 } => TypedResults.Unauthorized(), //"Service Unavailable"
            { Status: 504 } => TypedResults.Unauthorized(), //"Gateway Timeout"
            _ => TypedResults.BadRequest(er.Problem),
        };
}
