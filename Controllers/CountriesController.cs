using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using WrestleApplicationAPI.DbContexts;
using WrestleApplicationAPI.Entities;

//using WrestleApplicationAPI.Entities;
using WrestleApplicationAPI.Models;
using WrestleApplicationAPI.Services;

namespace WrestleApplicationAPI.Controllers
{
    [Route("api/continents/{continentId}/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<CountriesController> _logger;
        private readonly IMailService _mailService;
        private readonly ContinentsDataStore _continentsDataStore;

        public CountriesController(ILogger<CountriesController> logger, IMailService mailService, ContinentsDataStore continentsDataStore, DataContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _continentsDataStore = continentsDataStore ?? throw new ArgumentNullException(nameof(continentsDataStore));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CountryDTO>> GetCountries(int continentId)
        {
            try 
            {
                var continent = _continentsDataStore.Continents.FirstOrDefault(continent => continent.IdContinent == continentId);

                if (continent == null)
                {
                    _logger.LogInformation($"Continent with id {continentId} wasn't found when accessing countries.");
                    return NotFound();
                }

                return Ok(continent.Countries);
            }
            catch(Exception ex)
            { 
                _logger.LogCritical($"Exception while getting countries for continent with id {continentId}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }

            
        }

        [HttpGet("{countryid}", Name = "GetCountry")]
        public ActionResult<CountryDTO> GetCountry(int continentId, string countryId)
        {
            var continent = _continentsDataStore.Continents.FirstOrDefault(continent => continent.IdContinent == continentId);

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
            var continent = _continentsDataStore.Continents.FirstOrDefault(continent => continent.IdContinent == continentId);

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
            var continent = _continentsDataStore.Continents.FirstOrDefault(continent => continent.IdContinent == continentId);

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
            var continent = _continentsDataStore.Continents.FirstOrDefault(continent => continent.IdContinent == continentId);

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
            var continent = _continentsDataStore.Continents.FirstOrDefault(continent => continent.IdContinent == continentId);

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

            _mailService.Send("Country deleted.", $"Country {countryFromStore.NameCountry} with id {countryFromStore.IdCountry} was deleted.");
            return NoContent();
        }
    }
}
