﻿using FluentValidation;

namespace FoodOnline.Models;

public record StoreModel : IModel
{
    public required string Id { get; init; }

    public byte[] Rev { get; init; } = Array.Empty<byte>();
}

public sealed class StoreModelValidator : AbstractValidator<StoreModel>
{
    public StoreModelValidator()
    {
    }
}