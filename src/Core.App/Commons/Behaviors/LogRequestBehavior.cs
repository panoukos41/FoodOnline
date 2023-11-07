using Core.Abstractions.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Commons.Behaviors;

public sealed class LogRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IMessage
{
    private readonly ILoggerFor<TRequest>? loggerFor;

    public LogRequestBehavior(IServiceProvider serviceProvider)
    {
        loggerFor = serviceProvider.GetService<ILoggerFor<TRequest>>();
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
