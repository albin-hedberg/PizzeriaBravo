using MongoDB.Driver;

namespace PizzeriaBravo.OrderService.DataAccess.Utilities;

public class MongoDbConnection
{
    private static readonly string HostName = "mongodb";
    private static readonly string Port = "27017";
    private static readonly string DatabaseName = "PizzeriaBravo";

    public static IMongoDatabase GetDatabase()
    {
        var client = new MongoClient($"mongodb://{HostName}:{Port}");
        return client.GetDatabase(DatabaseName);
    }
}
