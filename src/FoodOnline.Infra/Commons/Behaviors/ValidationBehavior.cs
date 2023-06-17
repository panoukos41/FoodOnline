using FluentValidation;
using FluentValidation.Results;

namespace FoodOnline.Commons.Behaviors;

public sealed class ValidationBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage, IValid
    where TResponse : IResultDU
{
    private static readonly Type ErType = typeof(Result<>.Er);

    public ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        var validationContext = new ValidationContext<TMessage>(message);
        var validationResult = TMessage.Validator.Validate(validationContext);

        return validationResult.IsValid
            ? next(message, cancellationToken)
            : new(NewEr(validationResult.Errors));
    }

    private static TResponse NewEr(List<ValidationFailure> errors)
    {
        var type = typeof(TResponse).GetGenericArguments()[0];
        var erType = ErType.MakeGenericType(type);
        var problem = Problems.Validation(errors);

        return (TResponse)Activator.CreateInstance(erType, problem)!;
    }
}
