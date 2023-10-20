using Core;
using Core.Commons;
using FluentValidation.Validators;

namespace FluentValidation;

public static class FluentValidationMixins
{
    public static IRuleBuilder<T, Uuid> Uuid<T>(this IRuleBuilder<T, Uuid> builder)
        => builder
        .NotEmpty();

    public static IRuleBuilder<T, Guid> Guid<T>(this IRuleBuilder<T, Guid> builder)
        => builder
        .NotEmpty();

    public static IRuleBuilder<T, string> Email<T>(this IRuleBuilder<T, string> builder)
        => builder
        .NotEmpty()
        .EmailAddress()
        .Length(3, 25)
        .WithName("Email");

    public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> builder)
        => builder
        .NotEmpty()
        .MinimumLength(10)
        .WithName("Password");

    public static IRuleBuilder<T, Phone> Phone<T>(this IRuleBuilder<T, Phone> builder)
        => builder
        .NotEmpty()
        .WithName("Phone");
}

//public class NotEmptyValidator<T, TProperty> : PropertyValidator<T, TProperty>
//{

//    public override string Name => "NotEmptyValidator";

//    public override bool IsValid(ValidationContext<T> context, TProperty value)
//    {
//        switch (value)
//        {
//            case null:
//            case string s when string.IsNullOrWhiteSpace(s):
//            case ICollection { Count: 0 }:
//            case Array { Length: 0 }:
//            case IEnumerable e when !e.GetEnumerator().MoveNext():
//                return false;
//        }

//        return !EqualityComparer<TProperty>.Default.Equals(value, default);
//    }

//    protected override string GetDefaultMessageTemplate(string errorCode)
//    {
//        return Localized(errorCode, Name);
//    }
//}
