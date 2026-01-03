using RestCountries.Core.Entities;

namespace RestCountries.Core.Services;

public interface IImportCountriesRepository
{
    Task<BulkUpsertStatsInfo> BulkUpsertAsync(IEnumerable<Country> countries);
}


public class BulkUpsertStatsInfo
{
    public int CountriesInsertedCount { get; set; }
    public int CountriesUpdatedCount { get; set; }
    public int LanguagesInsertedCount { get; set; }
    public int LanguagesUpdatedCount { get; set; }
    public int CountryLanguagesInsertedCount { get; set; }
    public int CountryLanguagesUpdatedCount { get; set; }
}
