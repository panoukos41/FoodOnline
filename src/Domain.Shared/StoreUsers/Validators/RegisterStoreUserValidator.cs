﻿using FluentValidation;
using FoodOnline.Domain.StoreUsers.Requests;

namespace FoodOnline.Domain.StoreUsers.Validators
{
    public class RegisterStoreUserValidator : AbstractValidator<RegisterStoreUser>
    {
        public RegisterStoreUserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username can't be empty")
                .MinimumLength(5).WithMessage("Username must be at least {MinLength}")
                .MaximumLength(50).WithMessage("Username must be equal or less than {MaxLength}");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password can't be empty")
                .MinimumLength(10).WithMessage("Password must be at least {MinLength}")
                .MaximumLength(50).WithMessage("Password must be equal or less than {MaxLength}");
        }
    }
}