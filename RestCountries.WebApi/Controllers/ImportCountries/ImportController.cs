using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RestCountries.Core.Entities;
using RestCountries.Core.Services;

namespace RestCountries.WebApi.Controllers.ImportCountries;

[ApiController]
[Route("api/[controller]")]
public class ImportController : ControllerBase
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IImportCountriesRepository importCountriesRepository;

    public ImportController(IHttpClientFactory httpClientFactory,
        IImportCountriesRepository importCountriesRepository)
    {
        this.httpClientFactory = httpClientFactory;
        this.importCountriesRepository = importCountriesRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        var client = httpClientFactory.CreateClient("RestCountriesHttpClient");
        var response = await client.GetAsync("independent?status=true");

        var countriesDto = await response.Content.ReadFromJsonAsync<List<ImportCountryDto>>();
        var importStats = await BulkImportCountries(countriesDto);
        
        
        response.EnsureSuccessStatusCode();
        return Ok(countriesDto);
    }

    private async Task<BulkUpsertStatsInfo> BulkImportCountries(List<ImportCountryDto>? countriesDto)
    {
        var countries = new List<Country>();
        foreach (var countryDto in countriesDto)
        {
            var country = new Country(countryDto.cca2)
            {
                OfficialName = countryDto.name?.official,
                Name = countryDto.name?.common,
                Region = countryDto.region,
                Subregion = countryDto.subregion,
                Capital = countryDto.capital != null && countryDto.capital.Count > 0 ? countryDto.capital[0] : null,
                Population = countryDto.population,
                Area = countryDto.area,
                Flag = !string.IsNullOrEmpty(countryDto.flags?.png) ? countryDto.flags.png : countryDto.flags?.svg,
                Languages = countryDto.languages
                            .DistinctBy(l => l.Key)
                            .Select(l => new Language(l.Key, l.Value))
                            .ToList()
            };

            countries.Add(country);
        }
        return await importCountriesRepository.BulkUpsertAsync(countries);
    }
}
