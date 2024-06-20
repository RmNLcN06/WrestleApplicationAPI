using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using WrestleApplicationAPI.Datas;
using WrestleApplicationAPI.Entities;

//using WrestleApplicationAPI.Entities;
using WrestleApplicationAPI.Models;

namespace WrestleApplicationAPI.Controllers
{
    [Route("api/continents/{continentId}/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        /*private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }*/

        [HttpGet]
        public ActionResult<IEnumerable<CountryDTO>> GetCountries(int continentId)
        { 
            var continent = ContinentsDataStore.Current.Continents.FirstOrDefault(continent => continent.IdContinent == continentId);

            if (continent == null)
            {
                return NotFound("Wrong Continent.");
            }

            return Ok(continent.Countries);
        }

        [HttpGet("{countryid}", Name = "GetCountry")]
        public ActionResult<CountryDTO> GetCountry(int continentId, string countryId)
        {
            var continent = ContinentsDataStore.Current.Continents.FirstOrDefault(continent => continent.IdContinent == continentId);

            if (continent == null)
            {
                return NotFound("Wrong Continent.");
            }


            var country = continent.Countries.FirstOrDefault(c => c.IdCountry == countryId);

            if (country == null)
            {
                return NotFound("Wrong Country.");
            }

            return Ok(country);
        }

        [HttpPost]
        public ActionResult<CountryDTO> CreateCountry(int continentId, CountryCreationDTO country) 
        {
            var continent = ContinentsDataStore.Current.Continents.FirstOrDefault(continent => continent.IdContinent == continentId);

            if (continent == null)
            {
                return NotFound("Wrong Continent.");
            }

            var finalCountry = new CountryDTO()
            {
                IdCountry = country.IdCountry,
                NameCountry = country.NameCountry,
                UrlFlagCountry = country.UrlFlagCountry,
            };

            continent.Countries.Add(finalCountry);

            return CreatedAtRoute("GetCountry", 
                new
                {
                    continentId = continentId,
                    countryId = finalCountry.IdCountry,
                },
                finalCountry);
        }

        [HttpPut("{countryid}")]
        public ActionResult ModificationCountry(int continentId, string countryId, CountryModificationDTO country)
        {
            var continent = ContinentsDataStore.Current.Continents.FirstOrDefault(continent => continent.IdContinent == continentId);

            if (continent == null)
            {
                return NotFound("Wrong Continent.");
            }

            // Find country
            var countryFromStore = continent.Countries.FirstOrDefault(c => c.IdCountry == countryId);
            if (countryFromStore == null)
            { 
                return NotFound("Country from store not found.");
            }

            countryFromStore.IdCountry = country.IdCountry;
            countryFromStore.NameCountry = country.NameCountry;
            countryFromStore.UrlFlagCountry = country.UrlFlagCountry;

            return NoContent();
        }

        [HttpPatch("{countryid}")]
        public ActionResult PartialModificationCountry(int continentId, string countryId, JsonPatchDocument<CountryModificationDTO> patchDocument)
        {
            var continent = ContinentsDataStore.Current.Continents.FirstOrDefault(continent => continent.IdContinent == continentId);

            if (continent == null)
            {
                return NotFound("Wrong Continent.");
            }

            // Find country
            var countryFromStore = continent.Countries.FirstOrDefault(c => c.IdCountry == countryId);
            if (countryFromStore == null)
            {
                return NotFound("Country from store not found.");
            }

            var countryToPatch = new CountryModificationDTO()
            {
                IdCountry = countryFromStore.IdCountry,
                NameCountry = countryFromStore.NameCountry,
                UrlFlagCountry = countryFromStore.UrlFlagCountry
            };

            patchDocument.ApplyTo(countryToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(countryToPatch))
            {
                return BadRequest(ModelState);
            }

            countryFromStore.IdCountry = countryToPatch.IdCountry;
            countryFromStore.NameCountry = countryToPatch.NameCountry;
            countryFromStore.UrlFlagCountry = countryToPatch.UrlFlagCountry;

            return NoContent();
        }

        [HttpDelete("{countryid}")]
        public ActionResult DeleteCountry(int continentId, string countryId)
        {
            var continent = ContinentsDataStore.Current.Continents.FirstOrDefault(continent => continent.IdContinent == continentId);

            if (continent == null)
            {
                return NotFound("Wrong Continent.");
            }

            // Find country
            var countryFromStore = continent.Countries.FirstOrDefault(c => c.IdCountry == countryId);

            if (countryFromStore == null)
            {
                return NotFound("Country from store not found.");
            }

            continent.Countries.Remove(countryFromStore);
            return NoContent();
        }
    }
}
