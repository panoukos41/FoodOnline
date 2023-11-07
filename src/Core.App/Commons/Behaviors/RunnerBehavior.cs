using Core.Abstractions.Events;
using Core.Abstractions.Requests;

namespace Core.Commons.Behaviors;

public sealed class RunnerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IMessage
    where TResponse : IResultUnion
{
    private readonly IEventHandler<RequestSucceeded>[] successEventHandlers;
    private readonly IEventHandler<RequestFailed>[] failedEventHandlers;

    //private readonly IPublisher publisher;
    //private readonly UserPrincipal? user;

    public RunnerBehavior(
        IEnumerable<IEventHandler<RequestSucceeded>> successEventHandlers,
        IEnumerable<IEventHandler<RequestFailed>> failedEventHandlers)
    {
        this.successEventHandlers = successEventHandlers.ToArray();
        this.failedEventHandlers = failedEventHandlers.ToArray();
        //this.publisher = publisher;
        //this.user = user;
    }

    public async ValueTask<TResponse> Handle(TRequest request, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {
        try
        {
            var result = await next(request, cancellationToken);
            if (result.IsEr(out var problem))
            {
                Log.Error(problem.Exception, "RQST {RequestName}, Problem {Problem}", request.GetType().Name, problem);
                PublishRequestFailed(request);
            }
            PublishRequestSuccess(request);
            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "RQST {RequestName}", request.GetType().Name);
            PublishRequestFailed(request);
            return (TResponse)TResponse.CreateEr(ex);
        }
    }

    private void PublishRequestSuccess(TRequest request)
    {
        var @event = new RequestSucceeded
        {
            Request = request,
            Context = GetRequestContext()
        };
        foreach (var h in successEventHandlers)
        {
            // todo: Add logger on exception.
            FireAndForget(h.Handle(@event, default));
        }
    }

    private void PublishRequestFailed(TRequest request)
    {
        var @event = new RequestFailed
        {
            Request = request,
            Context = GetRequestContext()
        };
        foreach (var h in failedEventHandlers)
        {
            // todo: Add logger on exception.
            FireAndForget(h.Handle(@event, default));
        }
    }

    private RequestContext GetRequestContext() => new()
    {
        Items = new Dictionary<object, object?>(),
        User = new UserPrincipal()
    };

    static async void FireAndForget(
        ValueTask valueTask,
        Action<Exception>? onException = null,
        bool configureAwait = false,
        bool shouldRethrow = false)
    {
        try
        {
            await valueTask.ConfigureAwait(configureAwait);
        }
        catch (Exception ex) when (onException is not null)
        {
            onException(ex);
            if (shouldRethrow) throw;
        }
    }

    static async void FireAndForget(
        Task task,
        Action<Exception>? onException = null,
        bool configureAwait = false,
        bool shouldRethrow = false)
    {
        try
        {
            await task.ConfigureAwait(configureAwait);
        }
        catch (Exception ex) when (onException is not null)
        {
            onException(ex);
            if (shouldRethrow) throw;
        }
    }
}
