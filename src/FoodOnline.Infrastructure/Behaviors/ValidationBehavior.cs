using FluentValidation;
using FoodOnline.Abstractions;

namespace FoodOnline.Infrastructure.Behaviors;

public sealed class ValidationBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage, IValid<TMessage>
{
    public ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        var validationResult = TMessage.Validator.Validate(message);

        return validationResult.IsValid
            ? next(message, cancellationToken)
            : throw new ValidationException(validationResult.Errors);
    }
}
