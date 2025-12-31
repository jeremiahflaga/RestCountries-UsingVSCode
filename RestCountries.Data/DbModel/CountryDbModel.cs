namespace RestCountries.Data;

internal class CountryDbModel
{
    public int Id { get; set; }
    public string CCA2 { get; set; }
    public string? OfficialName { get; set; }
    public string? Name { get; set; }
    public string? Region { get; set; }
    public string? Subregion { get; set; }
    public string? Capital { get; set; }
    public int? Population { get; set; }
    public double? Area { get; set; }
    public string? Flag { get; set; }
    public ICollection<CountryLanguageDbModel>? CountryLanguages { get; set; }
}
