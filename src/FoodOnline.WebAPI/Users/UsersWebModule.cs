using FoodOnline.Abstractions;
using FoodOnline.Commons.Extensions;
using FoodOnline.Users.Requests;
using Mediator;

namespace FoodOnline.Users;

/// <summary>
/// This already adds <see cref="UsersInfraModule"/>
/// </summary>
public sealed class UsersWebModule : IWebModule
{
    public static void Add(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.Add<UsersInfraModule>(configuration);
    }

    public static void Use(WebApplication app)
    {
        var group = app.MapGroup("users");
        group.WithTags("Users");

        group.MapGet("{userId}", (string userId, ISender sender) =>
        {
            return sender.Send(new GetUser(Uuid.Parse(userId))).Ok();
        });

        group.MapPost("", (CreateUser createUser, ISender sender) =>
        {
            return sender.Send(createUser).Ok();
        });

        group.MapPut("", (UpdateUser updateUser, ISender sender) =>
        {
            return sender.Send(updateUser).Ok();
        });
    }
}
