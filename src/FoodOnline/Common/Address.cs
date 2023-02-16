using FluentValidation;

namespace FoodOnline.Common;

public sealed record Address : Valid<Address, AddresValidator>
{
}

public class AddresValidator : AbstractValidator<Address>
{
    public AddresValidator()
    {

    }
}