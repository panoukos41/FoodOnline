using FluentValidation.Results;
using System.Text.Json;

namespace Core;

public static class Problems
{
    public static Problem Validation { get; } = new()
    {
        Type = nameof(Validation),
        Title = nameof(Validation),
        Status = 400,
        Detail = "Validation problems have occurred with your model. Check the Metadata:ValidationErrors a list of ValidationFailure objects."
    };

    public static Problem NotFound { get; } = new()
    {
        Type = nameof(NotFound),
        Title = nameof(NotFound),
        Status = 404
    };

    public static Problem Conflict { get; } = new()
    {
        Type = nameof(Conflict),
        Title = nameof(Conflict),
        Status = 409
    };

    public static Problem Teapot { get; } = new()
    {
        Type = nameof(Teapot),
        Title = nameof(Teapot),
        Status = 418,
        Detail = "I'm a teapot"
    };

    public static Problem Internal { get; } = new()
    {
        Type = nameof(Internal),
        Title = "Internal service error",
        Status = 500
    };
}

public static class ProblemMixins
{
    public static Problem WithValidationErrors(this Problem validation, List<ValidationFailure> failures)
    {
        return WithMetadata(validation, "ValidationErrors", failures);
    }

    public static Problem WithMetadata<TMetadata>(this Problem problem, string key, TMetadata metadata)
    {
        problem = problem with { Metadata = [] };
        problem.Metadata![key] = JsonSerializer.SerializeToElement(metadata, Options.Json);
        return problem;
    }

    public static Problem WithMetadata(this Problem problem, Dictionary<string, object> metadata)
    {
        problem = problem with { Metadata = [] };
        foreach (var (k, v) in metadata)
        {
            problem.Metadata![k] = JsonSerializer.SerializeToElement(v, Options.Json);
        }
        return problem;
    }

    public static Problem AppendMetadata<TMetadata>(this Problem problem, string key, TMetadata metadata)
    {
        var oldMeta = problem.Metadata;
        problem = problem with { Metadata = new(capacity: oldMeta?.Count + 1 ?? 1) };

        if (oldMeta is not null)
        {
            foreach (var (k, v) in oldMeta)
            {
                problem.Metadata[k] = v;
            }
        }

        problem.Metadata![key] = JsonSerializer.SerializeToElement(metadata, Options.Json);
        return problem;
    }

    public static Problem AppendMetadata<TMetadata>(this Problem problem, Dictionary<string, object> metadata)
    {
        var oldMeta = problem.Metadata;
        problem = problem with { Metadata = new(capacity: oldMeta?.Count + 1 ?? 1) };

        if (oldMeta is not null)
        {
            foreach (var (k, v) in oldMeta)
            {
                problem.Metadata[k] = v;
            }
        }
        foreach (var (k, v) in metadata)
        {
            problem.Metadata![k] = JsonSerializer.SerializeToElement(v, Options.Json);
        }
        return problem;
    }
}
