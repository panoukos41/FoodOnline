using Serilog;

namespace FoodOnline.Infrastructure.Behaviors;

public sealed class LogBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    public ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        Console.WriteLine(message);

        Log.Information("RQST {Request}", message);

        return next(message, cancellationToken);
    }
}
