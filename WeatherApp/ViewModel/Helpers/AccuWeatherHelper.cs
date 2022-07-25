using Newtonsoft.Json;
using System.Net.Http;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers;

public class AccuWeatherHelper
{
    public const string BASE_URL = "http://dataservice.accuweather.com/";
    public const string AUTOCOMPLETE_ENDPOIN = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
    public const string CURRENTCONDITION_ENDPOIN = "currentconditions/v1/{0}?apikey={1}";
    public const string API_KEY = "UsSVDR9AlGXOQ7g2NtLyApSWIjscq8MG";

    public static async Task<List<City>> GetCities(string query)
    {
        List<City> cities = new();
        string url = BASE_URL+ string.Format(AUTOCOMPLETE_ENDPOIN, API_KEY, query);

        using (HttpClient client = new())
        {
            var response = await client.GetAsync(url);

            string json = await response.Content.ReadAsStringAsync();

            cities = JsonConvert.DeserializeObject<List<City>>(json);
        }

        return cities;
    }


    public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
    {
        CurrentConditions currentConditions = new CurrentConditions();

        string url = BASE_URL+ string.Format(CURRENTCONDITION_ENDPOIN, cityKey, API_KEY);

        using (HttpClient client = new())
        {
            var response = await client.GetAsync(url);

            string json = await response.Content.ReadAsStringAsync();

            currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();
        }

        return currentConditions;
    }
}
