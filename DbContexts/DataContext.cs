using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WrestleApplicationAPI.Entities;

namespace WrestleApplicationAPI.DbContexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Continent>()
                .HasMany(e => e.Countries)
                .WithOne(e => e.Continent)
                .HasForeignKey(e => e.ContinentId)
                .IsRequired();

            modelBuilder.Entity<Continent>()
                 .HasData(
                new Continent("Europe")
                {
                    IdContinent = 1
                },
                new Continent("Asia")
                {
                    IdContinent = 2
                },
                new Continent("Africa")
                {
                    IdContinent = 3
                },
                new Continent("North America")
                {
                    IdContinent = 4
                },
                new Continent("South America")
                {
                    IdContinent = 5
                },
                new Continent("Oceania")
                {
                    IdContinent = 6
                },
                new Continent("Antarctica")
                {
                    IdContinent = 7
                });

            modelBuilder.Entity<Country>()
                 .HasData(
                new Country("France")
                {
                    IdCountry = 1,
                    ShortNameCountry = "FRA",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/c/c3/Flag_of_France.svg?uselang=fr",
                    ContinentId = 1
                },
                new Country("Italy")
                {
                    IdCountry = 2,
                    ShortNameCountry = "ITA",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/0/03/Flag_of_Italy.svg?uselang=fr",
                    ContinentId = 1
                },
                new Country("China")
                {
                    IdCountry = 3,
                    ShortNameCountry = "CHN",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/f/fa/Flag_of_the_People%27s_Republic_of_China.svg?uselang=fr",
                    ContinentId = 2
                },
                new Country("Japan")
                {
                    IdCountry = 4,
                    ShortNameCountry = "JPN",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/9/9e/Flag_of_Japan.svg?uselang=fr",
                    ContinentId = 2
                },
                new Country("Algeria")
                {
                    IdCountry = 5,
                    ShortNameCountry = "DZA",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/7/77/Flag_of_Algeria.svg?uselang=fr",
                    ContinentId = 3
                },
                new Country("South Africa")
                {
                    IdCountry = 6,
                    ShortNameCountry = "ZAF",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/a/af/Flag_of_South_Africa.svg?uselang=fr",
                    ContinentId = 3
                },
                new Country("United States")
                {
                    IdCountry = 7,
                    ShortNameCountry = "USA",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/a/a4/Flag_of_the_United_States.svg?uselang=fr",
                    ContinentId = 4
                },
                new Country("Canada")
                {
                    IdCountry = 8,
                    ShortNameCountry = "CAN",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/c/cf/Flag_of_Canada.svg?uselang=fr",
                    ContinentId = 4
                },
                new Country("Brazil")
                {
                    IdCountry = 9,
                    ShortNameCountry = "BRA",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/0/05/Flag_of_Brazil.svg?uselang=fr",
                    ContinentId = 5
                },
                new Country("Argentina")
                {
                    IdCountry = 10,
                    ShortNameCountry = "ARG",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/1/1a/Flag_of_Argentina.svg?uselang=fr",
                    ContinentId = 5
                },
                new Country("Australia")
                {
                    IdCountry = 11,
                    ShortNameCountry = "AUS",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/b/b9/Flag_of_Australia.svg?uselang=fr",
                    ContinentId = 6
                },
                new Country("New Zealand")
                {
                    IdCountry = 12,
                    ShortNameCountry = "NZL",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/3/3e/Flag_of_New_Zealand.svg?uselang=fr",
                    ContinentId = 6
                },
                new Country("Antarctica")
                {
                    IdCountry = 13,
                    ShortNameCountry = "ATA",
                    UrlFlagCountry = "https://upload.wikimedia.org/wikipedia/commons/b/bb/Proposed_flag_of_Antarctica_%28Graham_Bartram%29.svg?uselang=fr",
                    ContinentId = 7
                });
        }
    }
}
