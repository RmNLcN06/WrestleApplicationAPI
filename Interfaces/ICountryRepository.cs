using WrestleApplicationAPI.Entities;
using WrestleApplicationAPI.Services;

namespace WrestleApplicationAPI.Interfaces
{
    public interface ICountryRepository
    {
        /// <summary>
        /// Get a list of countries
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Country>> GetCountriesAsync();




        /// <summary>
        /// Get a country by filtering
        /// </summary>
        /// <param name="nameCountry">Name of country to filter</param>
        /// <returns></returns>
        Task<(IEnumerable<Country>, PaginationMetadata)> GetCountriesAsync(string? nameCountry, string? searchQuery, int pageNumber, int pageSize);




        /// <summary>
        /// Get a country
        /// </summary>
        /// <param name="countryId">Country ID</param>
        /// <param name="includeCities">Show city(ies) included in Country</param>
        /// <returns></returns>
        Task<Country?> GetCountryAsync(int countryId, bool includeCities);




        /// <summary>
        /// Verify if country exist
        /// </summary>
        /// <param name="countryId">Country ID</param>
        /// <returns></returns>
        Task<bool> CountryExistsAsync(int countryId);




        /// <summary>
        /// Get a list of cities for a country
        /// </summary>
        /// <param name="countryId">Country ID</param>
        /// <returns></returns>
        Task<IEnumerable<City>> GetCitiesForCountryAsync(int countryId);




        /// <summary>
        /// Get a country for a continent
        /// </summary>
        /// <param name="countryId">Country ID</param>
        /// <param name="cityId">City ID</param>
        /// <returns></returns>
        Task<City?> GetCityForCountryAsync(int countryId, int cityId);




        /// <summary>
        /// Create city in specific country
        /// </summary>
        /// <param name="countryId">Country ID</param>
        /// <param name="city">City Entity</param>
        /// <returns></returns>
        Task AddCityForCountryAsync(int countryId, City city);




        /// <summary>
        /// Delete city in specific country
        /// </summary>
        /// <param name="city">City Entity</param>
        void DeleteCityForCountry(City city);




        /// <summary>
        /// Save changes for country ID
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();
    }
}
