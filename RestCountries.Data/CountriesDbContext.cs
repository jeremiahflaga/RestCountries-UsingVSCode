using Microsoft.EntityFrameworkCore;

namespace RestCountries.Data;

public class CountriesDbContext : DbContext
{
    internal DbSet<CountryDbModel> Countries => Set<CountryDbModel>();
    internal DbSet<LanguageDbModel> Languages => Set<LanguageDbModel>();

    public CountriesDbContext(DbContextOptions<CountriesDbContext> options) 
        : base(options) 
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CountryDbModel>()
            .Property(x => x.CCA2)
            .HasMaxLength(2);

        modelBuilder.Entity<LanguageDbModel>()
            .Property(x => x.Code)
            .HasMaxLength(3);

        modelBuilder.Entity<CountryLanguageDbModel>(x =>
        {
            x.ToTable("CountryLanguages");

            x.HasKey(x => new { x.CountryId, x.LanguageId });

            x.HasOne(x => x.Country)
                .WithMany(x => x.CountryLanguages)
                .HasForeignKey(x => x.CountryId);

            x.HasOne(x => x.Language)
                .WithMany(x => x.CountryLanguages)
                .HasForeignKey(x => x.LanguageId);
        });
    }
}
