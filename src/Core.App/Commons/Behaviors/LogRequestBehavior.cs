using Core.Abstractions.Requests;

namespace Core.Commons.Behaviors;

public sealed class LogRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private readonly ILoggerFor<TRequest>? loggerFor;

    public LogRequestBehavior(ILoggerFor<TRequest>? loggerFor)
    {
        this.loggerFor = loggerFor;
    }

    public ValueTask<TResponse> Handle(TRequest request, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {
        var logger = Log.ForContext<TRequest>();
        if (request is ISelfLog customLogging)
        {
            customLogging.Log(logger);
        }
        else if (loggerFor is { })
        {
            loggerFor.Log(request);
        }
        else
        {
            logger.Information("RQST {Request}", request);
        }

        return next(request, cancellationToken);
    }
}
