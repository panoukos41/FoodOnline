using FluentValidation;

namespace FoodOnline.Commons.Behaviors;

public sealed class ValidationBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage, IValid
{
    public ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        var validationContext = new ValidationContext<TMessage>(message);
        var validationResult = TMessage.Validator.Validate(validationContext);

        return validationResult.IsValid
            ? next(message, cancellationToken)
            : throw new ValidationException(validationResult.Errors);
    }
}
