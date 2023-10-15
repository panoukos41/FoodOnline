using FluentValidation;

namespace Core.Commons.Behaviors;

public sealed class ValidBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage, IValid
    where TResponse : IResultUnion
{
    public ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        var validationContext = new ValidationContext<TMessage>(message);
        var validationResult = TMessage.Validator.Validate(validationContext);

        return validationResult.IsValid
            ? next(message, cancellationToken)
            : new((TResponse)TResponse.CreateEr(Problems.Validation.WithValidationErrors(validationResult.Errors)));
    }
}
