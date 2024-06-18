using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WrestleApplicationAPI.Entities;

namespace WrestleApplicationAPI.Datas
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
        }
    }
}
