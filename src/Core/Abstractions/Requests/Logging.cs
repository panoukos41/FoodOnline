using Serilog;

namespace Core.Abstractions.Requests;

public interface ISelfLog
{
    void Log(ILogger logger);
}

public interface ILoggerFor<TData>
{
    void Log(TData data);
}