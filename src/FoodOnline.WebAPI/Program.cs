using FoodOnline.Auths;
using FoodOnline.Orders;
using FoodOnline.Stores;
using FoodOnline.Users;

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

builder.Add<AuthsWebModule>();
builder.Add<OrdersWebModule>();
builder.Add<StoresWebModule>();
builder.Add<UsersWebModule>();
builder.Add<CommonsWebModule>();

var app = builder.Build();

app.Use<AuthsWebModule>();
app.Use<OrdersWebModule>();
app.Use<StoresWebModule>();
app.Use<UsersWebModule>();
app.Use<CommonsWebModule>();

try
{
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Error(ex, "Failure");
}
finally
{
    await Log.CloseAndFlushAsync();
}
