using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace RestCountries.WebApi.Controllers.ImportCountries;

[ApiController]
[Route("api/[controller]")]
public class ImportController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ImportController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        var client = _httpClientFactory.CreateClient("RestCountriesHttpClient");
        var response = await client.GetAsync("independent?status=true");

        var content = await response.Content.ReadAsStringAsync();
        var countries = JsonSerializer.Deserialize<List<CountryDto>>(content);
        response.EnsureSuccessStatusCode();
        return Ok(countries);
    }
}
