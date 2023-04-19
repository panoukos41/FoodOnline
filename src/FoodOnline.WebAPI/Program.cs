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

services.ConfigureInfraModules(configuration);

builder.Host.UseSerilog();
builder.ConfigureWebModules();

var app = builder.Build();

app.UseHttpsRedirection();

// Add auth stuff before everything else.
app.UseWebModule<WebApiModule>();

// Use serilog for http requests.
app.UseSerilogRequestLogging();

// Configure the rest of web modules.
app.UseWebModules();

await app.RunAsync();
