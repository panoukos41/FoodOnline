var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    //.WriteTo.File($"{Environment.CurrentDirectory}/Logs/logs.txt")
#if DEBUG
    .MinimumLevel.Debug()
#else
    .MinimumLevel.Information()
#endif
    .CreateLogger();

builder.Host.UseSerilog();

services.AddInfrastructureModules(configuration);
builder.AddWebModules();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.UseAllWebModules();

await app.RunAsync();
