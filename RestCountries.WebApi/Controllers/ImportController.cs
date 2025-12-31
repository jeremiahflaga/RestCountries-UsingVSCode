using Microsoft.AspNetCore.Mvc;

namespace RestCountries.WebApi.Controllers
{
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
        public async Task<IActionResult> Import()
        {
            var client = _httpClientFactory.CreateClient("RestCountriesHttpClient");
            var response = await client.GetAsync("independent?status=true");
            response.EnsureSuccessStatusCode();
            return Ok();
        }
    }
}