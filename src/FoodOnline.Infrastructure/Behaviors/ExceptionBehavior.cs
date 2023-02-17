using Serilog;

namespace FoodOnline.Infrastructure.Behaviors;

public sealed class ExceptionBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
    where TResponse : IResult
{
    private static readonly Type Err = typeof(Result<>.Err);

    public async ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        try
        {
            return await next(message, cancellationToken);
        }
        catch (Exception ex)
        {
            var err = NewErr(ex);

            Log.Error(ex, "{Error}", err);

            return err;
        }
    }

    private static TResponse NewErr(Exception ex)
    {
        var type = typeof(TResponse).GetGenericArguments()[0];
        var err = Err.MakeGenericType(type);
        return (TResponse)Activator.CreateInstance(err, new[] { ex })!;
    }
}
