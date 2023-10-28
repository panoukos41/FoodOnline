using Core.Abstractions;
using Core.Commons;
using Core.MongoDb.Commons;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;

namespace Core;

public class CoreMongoDbAppModule : IAppModule<CoreMongoDbAppModule>, IValid
{
    public string DatabaseName { get; set; } = string.Empty;

    public string ConnectionString { get; set; } = string.Empty;

    public Func<IServiceProvider, UserPrincipal>? UserPrincipal { get; set; }

    public static void Add(IServiceCollection services, IConfiguration configuration, CoreMongoDbAppModule module)
    {
        module.ValidateAndThrow();

        if (module.UserPrincipal is { })
        {
            services.TryAddScoped(module.UserPrincipal);
        }

        //UuidBsonSerializer.TryRegister();

        services.AddSingleton(static sp =>
        {
            var module = sp.GetRequiredService<CoreMongoDbAppModule>();
            var client = new MongoClient(module.ConnectionString);
            return new MongoDbContext(client, module.DatabaseName, null, null);
        });
    }

    public static IValidator Validator { get; } = InlineValidator.For<CoreMongoDbAppModule>(data =>
    {
        data.RuleFor(x => x.DatabaseName)
            .NotEmpty()
            .Must(x => !x.Contains('.')).WithMessage("'{PropertyName}' must not contain the '.' character.");

        data.RuleFor(x => x.ConnectionString)
            .NotEmpty();
    });
}
