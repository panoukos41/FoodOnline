using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FoodOnline.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Abstractions
{
    public interface IValid
    {
        public abstract static IValidator Validator { get; }
    }

    public interface IValid<in T> : IValid
    {
        public abstract static new IValidator<T> Validator { get; }
    }
}

namespace FoodOnline
{
    public abstract record Valid<T, TValidator> : IValid<T>
        where TValidator : class, IValidator<T>, new()
    {
        private static readonly Lazy<TValidator> _validator = new(static () => new TValidator());

        public static IValidator<T> Validator => _validator.Value;

        static IValidator IValid.Validator => Validator;
    }

    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Necessary for extension methods.")]
    public static class IValidExtensions
    {
        public static ValidationResult Validate<T>(this T obj)
            where T : class, IValid<T>
        {
            return T.Validator.Validate(obj);
        }

        public static ValidationResult Validate<T>(this T obj, IValidationContext context)
            where T : class, IValid<T>
        {
            return T.Validator.Validate(context);
        }

        public static ValidationResult Validate<T>(this T obj, Action<ValidationStrategy<T>> options)
            where T : class, IValid<T>
        {
            return T.Validator.Validate(obj);
        }

        public static IValidator<T> GetValidator<T>(this T obj)
            where T : class, IValid<T>
        {
            return T.Validator;
        }
    }
}
