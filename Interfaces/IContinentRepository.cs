﻿using WrestleApplicationAPI.Entities;

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
        Task<Country?> GetCountryForContinentAsync(int continentId, string countryId);
    }
}