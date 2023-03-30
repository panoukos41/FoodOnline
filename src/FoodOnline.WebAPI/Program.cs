using FoodOnline;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    //.WriteTo.File($"{Environment.CurrentDirectory}/Logs/logs.txt")
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog();

//services.AddInfrastructure(configuration);

services.AddAllInfrastructureModules(configuration);

var app = builder.Build();

//app.UseInfrastructureServiceProvider();

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

//app.MapPost("/login", (LoginUser loginUser, ISender s) => s.Send(loginUser));
//app.MapGet("/user", () => "Hello");
//app.MapGet("/user/{id}", (string id, ISender s) => s.Send(new GetUser(Uuid.Parse(id))).Ok());

await app.RunAsync();
