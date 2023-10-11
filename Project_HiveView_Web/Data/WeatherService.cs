
namespace Project_HiveView_Web.Data;

public class WeatherService {
    public List<WeatherData> WeatherDataList { get; set; }
    public WeatherService() {
        WeatherDataList = LoadData();
    }
    
    private List<WeatherData> LoadData()
    {
        var current_date = DateTime.Now.Date.AddDays(-1);
        var startdate = current_date.AddDays(-30).Date;
        var MongoHandler = new MongoDB_Handler("mongodb://localhost:27017", "Hive_Vault_Dev");
        var WeatherDataList = new List<WeatherData>();
        for(int i = 0; i < 30; i++)
        {
            var data = MongoHandler.GetDocumentByFilter<WeatherData>("daily_weather_data", "date", startdate.ToString("yyyy-MM-dd"));
            WeatherDataList.Add(data);
            startdate = startdate.AddDays(1);
        }
        return WeatherDataList;
    }
}