using FluentValidation;
using FoodOnline.Abstractions.Requests;
using FoodOnline.Validation;
using Serilog;

namespace FoodOnline.Requests.AuthRequests;

public sealed record LoginUser : Command<string>, IValid, ISelfLogging
{
    public required string Email { get; set; }

    public required string Password { get; set; }

    public void Log(ILogger logger)
    {
        logger.Information("RQST LoginUser {{ Email = {Email} }}", Email);
    }

    public static IValidator Validator { get; } = new LoginUserValidator();
}

public sealed class LoginUserValidator : AbstractValidator<LoginUser>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).ValidEmail();
        RuleFor(x => x.Password).ValidPassword();
    }
}
