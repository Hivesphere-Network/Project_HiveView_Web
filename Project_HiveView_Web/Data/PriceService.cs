using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Project_HiveView_Web.Data;

public class PriceService
{
    public List<WeeklyPriceDM> WeeklyPriceList { get; set; }
    public List<string> Labels { get; set; }
    public PriceService() {
        WeeklyPriceList = LoadData();
        Labels = GetLabels();
    }
    public PriceService(string Product) {
        WeeklyPriceList = LoadData(Product);
        Labels = GetLabels();
    }
    
    private List<WeeklyPriceDM> LoadData(string Product = "Red Onion Imported")
    {
        var MongoHandler = new MongoDB_Handler("mongodb://localhost:27017", "Hive_Vault_Dev");
        var filters = new List<FilterDefinition<WeeklyPriceDM>>();
        filters.Add(Builders<WeeklyPriceDM>.Filter.Eq("Product", Product));
        var WeeklyPriceListBson = MongoHandler.GetDocumentsByFilter<BsonDocument>("weekly_prices");
        var WeeklyPriceList = new List<WeeklyPriceDM>();
        foreach (var bsonFile in WeeklyPriceListBson)
        {
            var WeeklyPrice = BsonSerializer.Deserialize<WeeklyPriceDM>(bsonFile);
            if (WeeklyPrice.MCode == Product)
            {
                WeeklyPriceList.Add(WeeklyPrice);
            }
        }
        return WeeklyPriceList;
    }
    
    private List<string> GetLabels()
    {
        return WeeklyPriceList.Select(item => item.Year + " " + item.Week).ToList();
    }
    
}