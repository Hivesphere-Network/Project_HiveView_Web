using MongoDB.Bson;
using MongoDB.Driver;

namespace Project_HiveView_Web.Data;

public class MongoDB_Handler
{
    private MongoClient Client { get; set; }
    private IMongoDatabase Database { get; set; }
    
    public MongoDB_Handler(string connectionString, string databaseName)
    {
        Client = new MongoClient(connectionString);
        Database = Client.GetDatabase(databaseName);
    }

    private IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return Database.GetCollection<T>(collectionName);
    }
    
    public T GetDocumentByFilter<T>(string collectionName,string filter_name, string filter_value)
    {
        var collection = GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq(filter_name, filter_value);
        return collection.Find(filter).FirstOrDefault();
    }
    
    public List<T> GetDocumentByDateRange<T>(string collectionName, DateTime startDate, DateTime endDate)
    {
        var collection = GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.And(
            Builders<T>.Filter.Gte("date", startDate),
            Builders<T>.Filter.Lte("date", endDate)
        );
        return collection.Find(filter).ToList();
    }
    
    public List<T> GetDocumentsByFilters<T>(string collectionName, List<FilterDefinition<T>> filters)
    {
        var collection = GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.And(filters);
        return collection.Find(filter).ToList();
    }
    
    public List<T> GetDocumentsByFilter<T>(string collectionName)
    {
        var collection = GetCollection<T>(collectionName);
        var productFilter = Builders<T>.Filter.Eq("Product", "Red Onion Imported");
        return collection.Find(productFilter).ToList();
    }
}