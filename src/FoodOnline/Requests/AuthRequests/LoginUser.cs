using FluentValidation;

namespace FoodOnline.Requests.AuthRequests;

public sealed record LoginUser : Valid<LoginUser, LoginUserValidator>, IRequest<string>
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}

public sealed class LoginUserValidator : AbstractValidator<LoginUser>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).ValidEmail();
        RuleFor(x => x.Password).ValidPassword();
    }
}
