namespace RestCountries.Data;

internal class CountryLanguageDbModel
{
    public int CountryId { get; set; }
    public CountryDbModel Country { get; set; }

    public int LanguageId { get; set; }
    public LanguageDbModel Language { get; set; }
}
