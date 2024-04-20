#region IMPORTS
using Alfasoft.Models;
using Newtonsoft.Json;
#endregion

namespace Alfasoft.Services;

public class CountriesService
{
    private static readonly HttpClient httpClient = new HttpClient();

    public async Task<List<Country>> GetCountriesAsync()
    {
        string apiUrl = "https://restcountries.com/v3.1/all";
        var response = await httpClient.GetStringAsync(apiUrl);
        var countries = JsonConvert.DeserializeObject<List<dynamic>>(response);
        return countries.Select(c => new Country { Name = c.name.common, Code = c.cca2 }).ToList();
    }
}
