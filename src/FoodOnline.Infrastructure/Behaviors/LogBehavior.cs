namespace FoodOnline.Infrastructure.Behaviors;

public sealed class LogBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IBaseRequest
{
    public ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        Console.WriteLine(message);

        return next(message, cancellationToken);
    }
}
