﻿using FluentValidation;

namespace FoodOnline.Users;

public sealed record User : IEntity
{
    public required Uuid Id { get; init; }

    public required string Username { get; set; }

    public required string Email { get; set; }

    public required Role Role { get; set; }

    public Phone Phone { get; set; } = Phone.Empty;

    public HashSet<Address> Addresses { get; set; } = new();

    public HashSet<Uuid> Favorites { get; set; } = new();

    public static IValidator Validator { get; } = new UserValidator();
}

public sealed class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Email).ValidEmail();
        RuleFor(x => x.Username).ValidName();
    }
}