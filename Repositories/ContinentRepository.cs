﻿using WrestleApplicationAPI.Entities;
using WrestleApplicationAPI.Interfaces;
using WrestleApplicationAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WrestleApplicationAPI.Services;

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

        public async Task<(IEnumerable<Continent>, PaginationMetadata)> GetContinentsAsync(string? nameContinent, string? searchQuery, int pageNumber, int pageSize)
        {
            // Collection to start from
            var collection = _context.Continents as IQueryable<Continent>;

            if (!string.IsNullOrWhiteSpace(nameContinent)) 
            {
                nameContinent = nameContinent.Trim();
                collection = collection.Where(continent => continent.NameContinent == nameContinent);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.NameContinent.Contains(searchQuery));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(continent => continent.NameContinent).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<Continent?> GetContinentAsync(int continentId, bool includeCountries)
        {
            if (includeCountries)
            { 
                return await _context.Continents.Include(continent => continent.Countries).Where(continent => continent.IdContinent == continentId).FirstOrDefaultAsync();
            }
            return await _context.Continents.Where(continent => continent.IdContinent == continentId).FirstOrDefaultAsync();
        }

        public async Task<Continent> AddContinentAsync(Continent continent)
        {
            _context.Continents.Add(continent);
            await _context.SaveChangesAsync();
            return continent;
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
