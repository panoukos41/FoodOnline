using Serilog;

namespace FoodOnline.Abstractions.Requests;

public interface ISelfLogging
{
    public void Log(ILogger logger);
}
