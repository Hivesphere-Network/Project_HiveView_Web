using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project_HiveView_Web.Data;

public class WeeklyPriceDM
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Product")]
    public string ProductName { get; set; }

    [BsonElement("MCODE")]
    public string MCode { get; set; }

    [BsonElement("Year")]
    public int Year { get; set; }

    [BsonElement("Week")]
    public string Week { get; set; }

    [BsonElement("Price")]
    public double Price { get; set; }
}