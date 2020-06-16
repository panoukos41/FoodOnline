using FoodOnline.Application.Auth.Requests;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain;
using FoodOnline.Domain.Stores.Requests;
using FoodOnline.Domain.StoreUsers.Requests;
using FoodOnline.Domain.ValueObjects;
using FoodOnline.Infrastructure.Persistence;
using FoodOnline.WebApi;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

[SetUpFixture]
public class Testing
{
    private static IConfigurationRoot configuration;
    private static IServiceScopeFactory scopeFactory;

    private static Checkpoint checkpoint;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

        configuration = builder.Build();

        var startup = new Startup(configuration);

        var services = new ServiceCollection();

        services.AddSingleton(Mock.Of<IWebHostEnvironment>(host =>
            host.EnvironmentName == "Development" &&
            host.ApplicationName == "FoodOnline.WebApi"));

        services.AddLogging();

        startup.ConfigureServices(services);

        // Setup testing user (need to add a user to identity and use a real guid)
        var currentUserServiceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(ICurrentUser));

        services.Remove(currentUserServiceDescriptor);

        services.AddTransient<ICurrentUser, CurrentUserService>();

        scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

        checkpoint = new Checkpoint
        {
            TablesToIgnore = new[] { "__EFMigrationsHistory" },
            DbAdapter = DbAdapter.MySql
        };

        EnsureDatabase();
    }

    private static void EnsureDatabase()
    {
        using var scope = scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        context.Database.Migrate();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetService<IMediator>();

        return await mediator.Send(request);
    }

    /// <summary>
    /// Get a random StoreUser of type owner, password is "aReallyStrongpassword"
    /// </summary>
    /// <returns></returns>
    public static Task<string> GetRandomOwner()
    {
        return SendAsync(new RegisterStoreUser
        {
            IsOwner = true,
            Username = IdGenerator.Generate(),
            Password = "aReallyStrongpassword"
        });
    }

    /// <summary>
    /// Get a random Store for an owner.
    /// </summary>
    /// <param name="ownerId"></param>
    /// <param name="address"></param>
    /// <returns></returns>
    public static Task<string> GetRandomStore(string ownerId, Address address = null)
    {
        return SendAsync(new RegisterStore
        {
            Name = IdGenerator.Generate(),
            OwnerId = ownerId,
            Address = address ?? new Address("A street", "in a region", "in a city", "in a country", "with a zipcode")
        });
    }

    /// <summary>
    /// Get a random StoreUser of type Employee, password is "aReallyStrongpassword"
    /// </summary>
    /// <param name="storeId"></param>
    /// <returns></returns>
    public static Task<string> GetRandomEmployee(string storeId)
    {
        return SendAsync(new RegisterStoreUser
        {
            IsOwner = false,
            StoreId = storeId,
            Username = IdGenerator.Generate(),
            Password = "aReallyStrongpassword"
        });
    }

    /// <summary>
    /// Get a random user.
    /// </summary>
    /// <returns></returns>
    public static Task<string> GetRandomUser()
    {
        return SendAsync(new CreateUser
        {
            Id = IdGenerator.Generate(),
            Email = IdGenerator.Generate(),
            Name = IdGenerator.Generate(),
            LoginProvider = "FoodOnline",
            ProviderDisplayName = "Food Online"
        });
    }

    private static string currentUserId;

    private class CurrentUserService : ICurrentUser
    {
        public string Id => currentUserId;
    }

    //public static async Task<string> RunAsDefaultUserAsync()
    //{
    //    return await RunAsUserAsync("test@local", "Testing1234!");
    //}

    //public static async Task<string> RunAsUserAsync(string userName, string password)
    //{
    //    using var scope = _scopeFactory.CreateScope();

    //    var userManager = scope.ServiceProvider.GetService<FoodOnlineUserManager>();

    //    var user = new User { Username = userName, Email = userName };

    //    var result = await userManager.CreateAsync(user, password);

    //    _currentUserId = user.Id;

    //    return _currentUserId;
    //}

    public static async Task ResetState()
    {
        await checkpoint.Reset(configuration.GetConnectionString("FoodOnlineAppDb"));
        currentUserId = null;
    }

    public static async Task<T> FindAsync<T>(string id) where T : class
    {
        using var scope = scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        return await context.FindAsync<T>(id);
    }

    public static async Task<List<T>> FindManyAsync<T>(params string[] ids) where T : class
    {
        using var scope = scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        var list = new List<T>();

        foreach (var id in ids)
        {
            var item = await context.FindAsync<T>(id);
            if (item != null)
            {
                list.Add(item);
            }
        }
        return list;
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}