using System.Reflection;

namespace Project_HiveView_Web.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class WeatherData
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("lat")]
    public double Latitude { get; set; }

    [BsonElement("lon")]
    public double Longitude { get; set; }

    [BsonElement("tz")]
    public string TimeZone { get; set; }

    [BsonElement("date")]
    public string Date { get; set; }

    [BsonElement("units")]
    public string Units { get; set; }

    [BsonElement("cloud_cover")]
    public CloudCover CloudCover { get; set; }

    [BsonElement("humidity")]
    public Humidity Humidity { get; set; }

    [BsonElement("precipitation")]
    public Precipitation Precipitation { get; set; }

    [BsonElement("temperature")]
    public Temperature Temperature { get; set; }

    [BsonElement("pressure")]
    public Pressure Pressure { get; set; }

    [BsonElement("wind")]
    public Wind Wind { get; set; }

    public object GetPropertyValue(string propertyName)
    {
        Type type = typeof(WeatherData);
        PropertyInfo property = type.GetProperty(propertyName);

        if (property != null)
        {
            return property.GetValue(this);
        }

        throw new ArgumentException($"Property with name '{propertyName}' not found in WeatherData.");
    }
}

public class CloudCover
{
    [BsonElement("afternoon")]
    public double Afternoon { get; set; }
}

public class Humidity
{
    [BsonElement("afternoon")]
    public double Afternoon { get; set; }
}

public class Precipitation
{
    [BsonElement("total")]
    public double Total { get; set; }
}

public class Temperature
{
    [BsonElement("min")]
    public double Min { get; set; }

    [BsonElement("max")]
    public double Max { get; set; }

    [BsonElement("afternoon")]
    public double Afternoon { get; set; }

    [BsonElement("night")]
    public double Night { get; set; }

    [BsonElement("evening")]
    public double Evening { get; set; }

    [BsonElement("morning")]
    public double Morning { get; set; }
    
    public object GetPropertyValue(string propertyName)
    {
        Type type = typeof(Temperature);
        PropertyInfo property = type.GetProperty(propertyName);

        if (property != null)
        {
            return property.GetValue(this);
        }

        throw new ArgumentException($"Property with name '{propertyName}' not found in Temperature.");
    }
}

public class Pressure
{
    [BsonElement("afternoon")]
    public double Afternoon { get; set; }
}

public class Wind
{
    [BsonElement("max")]
    public MaxWind Max { get; set; }
}

public class MaxWind
{
    [BsonElement("speed")]
    public double Speed { get; set; }

    [BsonElement("direction")]
    public double Direction { get; set; }
}