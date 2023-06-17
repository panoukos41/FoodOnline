﻿namespace FoodOnline.Commons.Behaviors;

public sealed class RunnerBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
    where TResponse : IResultDU
{
    private static readonly Type ErType = typeof(Result<>.Er);

    public async ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
    {
        try
        {
            return await next(message, cancellationToken);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "RQST {RequestName}", message.GetType().Name);
            return NewEr(ex);
        }
    }

    private static TResponse NewEr(Exception ex)
    {
        var type = typeof(TResponse).GetGenericArguments()[0];
        var erType = ErType.MakeGenericType(type);

        return (TResponse)Activator.CreateInstance(erType, (Problem)ex)!;
    }
}
