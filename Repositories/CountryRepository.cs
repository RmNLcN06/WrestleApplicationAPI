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

        public Task AddCityForCountryAsync(int countryId, City city)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CountryExistsAsync(int countryId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCityForCountry(City city)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<City>> GetCitiesForCountryAsync(int countryId)
        {
            throw new NotImplementedException();
        }

        public Task<City?> GetCityForCountryAsync(int countryId, int cityId)
        {
            throw new NotImplementedException();
        }

        public Task<Country?> GetCountryAsync(int countryId, bool includeCities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
