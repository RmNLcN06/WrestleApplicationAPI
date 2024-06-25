using WrestleApplicationAPI.Entities;

namespace WrestleApplicationAPI.Interfaces
{
    public interface IContinentRepository
    {
        /// <summary>
        /// Get a list of continents
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Continent>> GetContinentsAsync();

        /// <summary>
        /// Get a continent
        /// </summary>
        /// <param name="continentId">Continent ID</param>
        /// <param name="includeCountries">Show country(ies) included in Continent</param>
        /// <returns></returns>
        Task<Continent?> GetContinentAsync(int continentId, bool includeCountries);

        /// <summary>
        /// Verify if continent exist
        /// </summary>
        /// <param name="continentId">Continent ID</param>
        /// <returns></returns>
        Task<bool> ContinentExistsAsync(int continentId);

        /// <summary>
        /// Get a list of countries for a continent
        /// </summary>
        /// <param name="continentId">Continent ID</param>
        /// <returns></returns>
        Task<IEnumerable<Country>> GetCountriesForContinentAsync(int continentId);

        /// <summary>
        /// Get a country for a continent
        /// </summary>
        /// <param name="continentId">Continent ID</param>
        /// <param name="countryId">Country ID</param>
        /// <returns></returns>
        Task<Country?> GetCountryForContinentAsync(int continentId, int countryId);

        /// <summary>
        /// Create country in specific continent
        /// </summary>
        /// <param name="continentId">Continent ID</param>
        /// <param name="country">Country ID</param>
        /// <returns></returns>
        Task AddCountryForContinentAsync(int continentId, Country country);

        /// <summary>
        /// Save changes for continent ID
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();
    }
}
