using FluentValidation;
using FoodOnline.Commons;
using FoodOnline.Commons.BsonSerializesrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace FoodOnline.App.MongoDb;

public class MongoDbAppModule : IAppModule<MongoDbAppModule>, IValid
{
    public string DatabaseName { get; set; } = string.Empty;

    public string ConnectionString { get; set; } = string.Empty;

    public Func<IServiceProvider, UserPrincipal> UserPrincipal { get; set; } = null!;

    public static void Add(IServiceCollection services, IConfiguration configuration, MongoDbAppModule module)
    {
        if (module.Validate().Errors is { Count: > 0 } errors)
        {
            throw new ValidationException($"Add {nameof(MongoDbAppModule)}", errors, true);
        }

        services.AddScoped(module.UserPrincipal);
        services.AddMediator(static options => options.ServiceLifetime = ServiceLifetime.Singleton);

        UuidBsonSerializer.TryRegister();

        services.AddSingleton(static sp =>
        {
            var module = sp.GetRequiredService<MongoDbAppModule>();
            var client = new MongoClient(module.ConnectionString);
            return new MongoDbContext(client, module.DatabaseName, null, null);
        });
    }

    public static IValidator Validator { get; } = new InlineValidator<MongoDbAppModule>
    {
        static model => model.RuleFor(x => x.DatabaseName).NotEmpty(),
        static model => model.RuleFor(x => x.ConnectionString).NotEmpty(),
        static model => model.RuleFor(x => x.UserPrincipal).NotEmpty(),
    };
}
