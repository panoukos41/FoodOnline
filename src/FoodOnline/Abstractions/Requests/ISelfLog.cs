using Serilog;

namespace FoodOnline.Abstractions.Requests;

public interface ISelfLog
{
    public void Log(ILogger logger);
}
