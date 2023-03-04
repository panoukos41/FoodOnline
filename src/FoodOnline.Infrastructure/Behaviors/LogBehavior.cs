using FoodOnline.Abstractions.Requests;
using Serilog;

namespace FoodOnline.Infrastructure.Behaviors;

public sealed class LogBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    public ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        if (message is ISelfLogging customLogging)
        {
            customLogging.Log(Log.Logger);
        }
        else
        {
            Log.Information("RQST {Request}", message);
        }

        return next(message, cancellationToken);
    }
}
