using FluentValidation;
using FluentValidation.Results;

namespace Core.Commons.Behaviors;

public sealed class FluentValidationBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
    where TResponse : IResultUnion
{
    private readonly IEnumerable<IValidator<TMessage>>? validators;

    public FluentValidationBehavior(IEnumerable<IValidator<TMessage>>? validators)
    {
        this.validators = validators;
    }

    public async ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        if (validators is not null)
        {
            IValidationContext validationContext = new ValidationContext<TMessage>(message);
            var validationResults = new List<ValidationFailure>();

            foreach (var validator in validators)
            {
                var result = await validator.ValidateAsync(validationContext, cancellationToken);
                validationResults.AddRange(result.Errors);
            }

            if (validationResults.Count > 0)
            {
                return (TResponse)TResponse.CreateEr(Problems.Validation.WithValidationErrors(validationResults));
            }
        }

        return await next(message, cancellationToken);
    }
}
