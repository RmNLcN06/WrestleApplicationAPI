using Microsoft.EntityFrameworkCore;
using WrestleApplicationAPI.DbContexts;
using WrestleApplicationAPI.Entities;
using WrestleApplicationAPI.Interfaces;
using WrestleApplicationAPI.Services;

namespace WrestleApplicationAPI.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _context.Countries.OrderBy(country => country.FullNameCountry).ToListAsync();
        }

        public async Task<(IEnumerable<Country>, PaginationMetadata)> GetCountriesAsync(string? nameCountry, string? searchQuery, int pageNumber, int pageSize)
        {
            // Collection to start from
            var collection = _context.Countries as IQueryable<Country>;

            if (!string.IsNullOrWhiteSpace(nameCountry))
            {
                nameCountry = nameCountry.Trim();
                collection = collection.Where(country => country.FullNameCountry == nameCountry);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.FullNameCountry.Contains(searchQuery));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(country => country.FullNameCountry).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task AddCityForCountryAsync(int countryId, City city)
        {
            var country = await GetCountryAsync(countryId, false);
            if (country != null) 
            { 
                country.Cities.Add(city);
            }
        }

        public async Task<bool> CountryExistsAsync(int countryId)
        {
            return await _context.Countries.AnyAsync(country => country.IdCountry == countryId);
        }

        public void DeleteCityForCountry(City city)
        {
            _context.Cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetCitiesForCountryAsync(int countryId)
        {
            return await _context.Cities.Where(city => city.CountryId == countryId).ToListAsync();
        }

        public async Task<City?> GetCityForCountryAsync(int countryId, int cityId)
        {
            return await _context.Cities.Where(city => city.CountryId == countryId && city.IdCity == cityId).FirstOrDefaultAsync();
        }

        public async Task<Country?> GetCountryAsync(int countryId, bool includeCities)
        {
            if(includeCities)
            {
                return await _context.Countries.Include(country => country.Cities).Where(country => country.IdCountry == countryId).FirstOrDefaultAsync();
            }
            return await _context.Countries.Where(country => country.IdCountry == countryId).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
