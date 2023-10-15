using Amazon.Runtime;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using FluentValidation.Results;
using FoodOnline;
using FoodOnline.Auths;
using FoodOnline.Commons.Behaviors;
using FoodOnline.Orders;
using FoodOnline.Users;
using FoodOnline.Users.Requests;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Serilog;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

Log.Logger = new LoggerConfiguration().CreateLogger();

//services.ConfigureAppModules(configuration);

//var app = builder.Build();

AuthType auth1 = new AuthType.Default();
AuthType auth2 = new AuthType.GitHub();
AuthType auth3 = new AuthType.Facebook();

var jauth1 = JsonSerializer.Serialize(auth1);
var jauth2 = JsonSerializer.Serialize(auth2);
var jauth3 = JsonSerializer.Serialize(auth3);

Console.WriteLine($"""
    A1: {jauth1}
    A2: {jauth2}
    A3: {jauth3}
    """);

auth1 = JsonSerializer.Deserialize<AuthType>(jauth1)!;
auth2 = JsonSerializer.Deserialize<AuthType>(jauth2)!;
auth3 = JsonSerializer.Deserialize<AuthType>(jauth3)!;

Console.WriteLine($"""
    A1: {auth1}
    A2: {auth2}
    A3: {auth3}
    """);

var writer = new BsonDocumentWriter(new());

var bauth1 = new BsonDocumentWriter(new());
var bauth2 = new BsonDocumentWriter(new());
var bauth3 = new BsonDocumentWriter(new());

BsonSerializer.Serialize(bauth1, auth1);
BsonSerializer.Serialize(bauth2, auth2);
BsonSerializer.Serialize(bauth3, auth3);

Console.WriteLine($"""
    A1: {bauth1.Document}
    A2: {bauth2.Document}
    A3: {bauth3.Document}
    """);

auth1 = BsonSerializer.Deserialize<AuthType>(bauth1.Document)!;
auth2 = BsonSerializer.Deserialize<AuthType>(bauth2.Document)!;
auth3 = BsonSerializer.Deserialize<AuthType>(bauth3.Document)!;

Console.WriteLine($"""
    A1: {auth1}
    A2: {auth2}
    A3: {auth3}
    """);

Result<int> rInt = 10;
Result<string> rStr = "noice";
Result<Uuid> rUui = Uuid.NewUuid();

var jInt = JsonSerializer.Serialize(rInt);
var jStr = JsonSerializer.Serialize(rStr);
var jUui = JsonSerializer.Serialize(rUui);

Console.WriteLine($"""
    RI: {jInt}
    RS: {jStr}
    RU: {jUui}
    """);


rInt = JsonSerializer.Deserialize<Result<int>>(jInt)!;
rStr = JsonSerializer.Deserialize<Result<string>>(jStr)!;
rUui = JsonSerializer.Deserialize<Result<Uuid>>(jUui)!;

Console.WriteLine($"""
    RI: {rInt}
    RS: {rStr}
    RU: {rUui}
    """);

Result<Order> r1 = new Order { Id = Uuid.NewUuid(), UserId = Uuid.NewUuid(), StoreId = Uuid.NewUuid() };
Result<Order> r2 = new NotSupportedException();

Console.WriteLine($"""
    R1: {r1}
    R2: {r2}
    """);

var j1 = JsonSerializer.Serialize(r1);
var j2 = JsonSerializer.Serialize(r2);

Console.WriteLine($"""
    J1: {j1}
    J2: {j2}
    """);

var r3 = JsonSerializer.Deserialize<Result<Order>>(j1)!;
var r4 = JsonSerializer.Deserialize<Result<Order>>(j2)!;

Console.WriteLine($"""
    R3: {r3}
    R4: {r4}
    """);

Console.WriteLine($"""
    E1: {r1 == r3}
    E2: {r2 == r4}
    """);

Console.WriteLine($"""
    H1: {r1.GetHashCode()}
    H3: {r3.GetHashCode()}
    H2: {r2.GetHashCode()}
    H4: {r4.GetHashCode()}
    """);

//var sender = app.Services.GetRequiredService<ISender>();

//var create = await sender.Send(new SetUser(new()
//{
//    Id = Uuid.NewUuid(),
//    Email = "aa@ooo.com",
//    Name = "Test",
//    Role = Role.Admin
//}));

//if (create.IsOk(out var user))
//{
//    Console.WriteLine($"created: {user}");
//}
//else return;

//var find = await sender.Send(new GetUser(user.Id));

//if (find.IsOk(out var user2))
//{
//    Console.WriteLine($"found: {user2}");
//}
//else return;

//var delete = await sender
//    .Send(new DeleteUser(user.Id))
//    .MatchAsync(ok => "Deleted user", er => "Failed");

//Console.WriteLine(delete);
