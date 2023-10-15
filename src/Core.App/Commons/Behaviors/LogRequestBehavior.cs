using Core.Abstractions.Requests;

namespace Core.Commons.Behaviors;

public sealed class LogRequestBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    private readonly ILoggerFor<TMessage>? loggerFor;

    public LogRequestBehavior(ILoggerFor<TMessage>? loggerFor)
    {
        this.loggerFor = loggerFor;
    }

    public ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        var logger = Log.ForContext<TMessage>();
        if (message is ISelfLog customLogging)
        {
            customLogging.Log(logger);
        }
        else if (loggerFor is { })
        {
            loggerFor.Log(message);
        }
        else
        {
            logger.Information("RQST {Request}", message);
        }

        return next(message, cancellationToken);
    }
}
