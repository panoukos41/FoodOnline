﻿using FluentValidation;
using FoodOnline.Common;

namespace FoodOnline.Models;

public sealed record UserModel : Valid<UserModel, UserModelValidator>, IModel
{
    public required string Id { get; init; }

    public byte[] Rev { get; init; } = Array.Empty<byte>();

    public required string Email { get; set; }

    public required string Name { get; set; }

    public required Role Role { get; set; }
}

public sealed class UserModelValidator : AbstractValidator<UserModel>
{
    public UserModelValidator()
    {
        RuleFor(x => x.Email).ValidEmail();
        RuleFor(x => x.Name).ValidName();
    }
}