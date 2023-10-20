using Core.Abstractions.Requests;

namespace Core.Commons.Behaviors;

public sealed class RunnerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : IResultUnion
{
    private readonly IPublisher publisher;
    private readonly UserPrincipal? user;

    public RunnerBehavior(IPublisher publisher, UserPrincipal? user)
    {
        this.publisher = publisher;
        this.user = user;
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
        _ = publisher.Publish(@event).AsTask().ConfigureAwait(false);
    }

    private void PublishRequestFailed(TRequest request)
    {
        var @event = new RequestFailed
        {
            Request = request,
            Context = GetRequestContext()
        };
        _ = publisher.Publish(@event).AsTask().ConfigureAwait(false);
    }

    private RequestContext GetRequestContext() => new()
    {
        Items = new Dictionary<object, object?>(),
        User = user,
    };
}
