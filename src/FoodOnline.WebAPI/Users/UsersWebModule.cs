using FoodOnline.Abstractions;
using FoodOnline.Commons.Extensions;
using FoodOnline.Users.Requests;
using Mediator;

namespace FoodOnline.Users;

public sealed class UsersWebModule : IWebModule
{
    public static bool Configured { get; private set; }

    public static bool Used { get; private set; }

    public static void Configure(WebApplicationBuilder builder)
    {
        Configured = true;
    }

    public static void Use(WebApplication app)
    {
        Used = true;

        var group = app.MapGroup("users");
        group.WithTags("Users");

        group.MapGet("{id}", (string id, ISender sender) =>
        {
            return sender.Send(new GetUser(Uuid.Parse(id))).Ok();
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
