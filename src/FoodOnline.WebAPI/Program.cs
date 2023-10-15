using Core.Commons;
using FoodOnline.App.MongoDb;
using FoodOnline.Orders;
using FoodOnline.Stores;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Always add logging first.
builder.AddWebModule<LoggingWebModule>();

builder.AddWebModule<CoreWebModule>();
builder.AddWebModule<OrdersWebModule>();
builder.AddWebModule<StoresWebModule>();
builder.AddWebModule<SwaggerWebModule>(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
});

services.AddAppModule<MongoDbAppModule>(configuration, module =>
{
    module.DatabaseName = "db";
    module.ConnectionString = "aaa";
    module.UserPrincipal = (sp) =>
    {
        var httpContext = sp.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
        ArgumentNullException.ThrowIfNull(httpContext, nameof(httpContext));
        return new FoodOnline.Commons.UserPrincipal(httpContext.User);
    };
});

services
    .AddCors(cors => cors
    .AddDefaultPolicy(policy => policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .WithExposedHeaders("Content-Disposition")));

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddAuthorization(options =>
{
});

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseWebModule<SwaggerWebModule>();
}
else
{
    // use hts etc.
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseWebModule<CoreWebModule>();
app.UseWebModule<LoggingWebModule>();

app.UseWebModule<OrdersWebModule>();
app.UseWebModule<StoresWebModule>();

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
