using FluentValidation;

namespace Identity.RoleTypes;

public sealed record RoleType : IValid
{
    public required string Name { get; init; }

    public string? Description { get; init; }

    public static IValidator Validator { get; } = InlineValidator.For<RoleType>(data =>
    {
        data.RuleFor(x => x.Name)
            .Length(3, 50)
            .When(x => x.Name is { });

        data.RuleFor(x => x.Description)
            .Length(3, 250)
            .When(x => x.Description is { });
    });
}
