using Microsoft.EntityFrameworkCore;

namespace RestCountries.Data;

public class CountriesDbContext : DbContext
{
    public CountriesDbContext(DbContextOptions<CountriesDbContext> options) 
        : base(options) 
    {
    }
}
