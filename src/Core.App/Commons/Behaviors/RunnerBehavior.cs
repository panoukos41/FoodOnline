namespace Core.Commons.Behaviors;

public sealed class RunnerBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
    where TResponse : IResultUnion
{
    public async ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        try
        {
            var result = await next(message, cancellationToken);
            if (result.IsEr(out var problem))
            {
                Log.Error(problem.Exception, "RQST {RequestName}, Problem {Problem}", message.GetType().Name, problem);
            }
            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "RQST {RequestName}", message.GetType().Name);
            return (TResponse)TResponse.CreateEr(ex);
        }
    }
}
