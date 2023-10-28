using FluentValidation;

namespace Identity.ScopeTypes;

public sealed record ScopeType : IValid
{
    public required string Id { get; init; }

    public LocalizedString Name { get; init; } = [];

    public LocalizedString Description { get; init; } = [];

    public HashSet<string> Claims { get; init; } = [];

    public static IValidator Validator { get; } = InlineValidator.For<ScopeType>(data =>
    {
        data.RuleFor(x => x.Id)
            .NotEmpty();

        // todo: More checks on name/description
        //data.RuleFor(x => x.Name)
        //    .NotEmpty()
        //    .Length(3, 50)
        //    .When(x => x.Name is not null);

        //data.RuleFor(x => x.Description)
        //    .NotEmpty()
        //    .Length(1, 250)
        //    .When(x => x.Description is not null);
    });
}
