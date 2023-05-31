var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
#if DEBUG
    .MinimumLevel.Debug()
#else
    .WriteTo.File($"{Environment.CurrentDirectory}/Logs/logs.txt")
    .MinimumLevel.Information()
#endif
    .CreateLogger();

builder.Host.UseSerilog();

//services.ConfigureInfraModules(configuration);
//builder.ConfigureWebModules();

var app = builder.Build();

// Add application specific stuff like:
// auth, logging swagger.
//app.UseWebModule<WebApiModule>();

await app.RunAsync();
