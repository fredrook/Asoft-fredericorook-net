#region IMPORTS
using Alfasoft.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#endregion

namespace Alfasoft.Services;

public class CountriesService
{
    private static readonly HttpClient httpClient = new HttpClient();

    public async Task<List<Country>> GetCountriesAsync()
    {
        string apiUrl = "https://restcountries.com/v3.1/all";
        var response = await httpClient.GetStringAsync(apiUrl);
        var countriesJson = JArray.Parse(response);
        return countriesJson.Select(c => new Country
        {
            Name = c["name"]["common"].ToString(),
            Code = c["cca2"].ToString()
        }).ToList();
    }
}
