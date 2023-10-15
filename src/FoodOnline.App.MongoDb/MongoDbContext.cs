using MongoDB.Driver;

namespace FoodOnline.App.MongoDb;

public sealed class MongoDbContext
{
    private readonly MongoCollectionSettings? collectionSettings;

    public MongoClient Client { get; }

    public IMongoDatabase Database { get; }

    public MongoDbContext(MongoClient client, string databaseName, MongoDatabaseSettings? databaseSettings = null, MongoCollectionSettings? collectionSettings = null)
    {
        Client = client;
        Database = Client.GetDatabase(databaseName, databaseSettings);
        this.collectionSettings = collectionSettings;
    }

    public IMongoCollection<TDocument> GetCollection<TDocument>(string? collection = null, MongoCollectionSettings? options = null)
    {
        return Database.GetCollection<TDocument>(collection, options ?? collectionSettings);
    }
}
