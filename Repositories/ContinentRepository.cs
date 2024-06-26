using WrestleApplicationAPI.Entities;
using WrestleApplicationAPI.Interfaces;
using WrestleApplicationAPI.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace WrestleApplicationAPI.Repositories
{
    public class ContinentRepository : IContinentRepository
    {

        private readonly DataContext _context;

        public ContinentRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Continent>> GetContinentsAsync()
        {
            return await _context.Continents.OrderBy(continent => continent.NameContinent).ToListAsync();
        }

        public async Task<IEnumerable<Continent>> GetContinentsAsync(string? nameContinent)
        {
            if(string.IsNullOrEmpty(nameContinent))
            {
                return await GetContinentsAsync();
            }

            nameContinent = nameContinent.Trim();
            return await _context.Continents.Where(continent => continent.NameContinent == nameContinent).OrderBy(continent => continent.NameContinent).ToListAsync();
        }

        public async Task<Continent?> GetContinentAsync(int continentId, bool includeCountries)
        {
            if (includeCountries)
            { 
                return await _context.Continents.Include(continent => continent.Countries).Where(continent => continent.IdContinent == continentId).FirstOrDefaultAsync();
            }
            return await _context.Continents.Where(continent => continent.IdContinent == continentId).FirstOrDefaultAsync();
        }

        public async Task<bool> ContinentExistsAsync(int continentId)
        {
            return await _context.Continents.AnyAsync(continent => continent.IdContinent == continentId);
        }

        public async Task<IEnumerable<Country>> GetCountriesForContinentAsync(int continentId)
        {
            return await _context.Countries.Where(country => country.ContinentId == continentId).ToListAsync();
        }

        public async Task<Country?> GetCountryForContinentAsync(int continentId, int countryId)
        {
            return await _context.Countries.Where(country => country.ContinentId == continentId && country.IdCountry == countryId).FirstOrDefaultAsync();
        }

        public async Task AddCountryForContinentAsync(int continentId, Country country)
        {
            var continent = await GetContinentAsync(continentId, false);
            if (continent != null)
            {
                continent.Countries.Add(country);
            }
        }

        public void DeleteCountryForContinent(Country country) 
        { 
            _context.Countries.Remove(country);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
