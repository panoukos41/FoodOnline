using FoodOnline.Abstractions;
using FoodOnline.Users;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace FoodOnline.Commons;

/// <summary>
/// This already adds <see cref="CommonInfraModule"/>
/// </summary>
public class CommonsWebModule : IWebModule
{
    public static void Add(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.Add<CommonInfraModule>(configuration);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();
            options.MapType<Uuid>(() => new OpenApiSchema
            {
                Type = "string",
                Default = new OpenApiString(string.Empty)
            });
            options.MapType<Role>(() => new OpenApiSchema
            {
                Type = "object",
                Default = new OpenApiObject() { { "$role", new OpenApiString("User") } },
            });
        });

        //services.AddAuthentication();
        //services.AddAuthorization();
    }

    public static void Use(WebApplication app)
    {
        app.UseHttpsRedirection();

        //app.UseAuthentication();
        //app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "docs";
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
        });

        app.UseSerilogRequestLogging();
    }
}
