using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using WrestleApplicationAPI.DbContexts;
using WrestleApplicationAPI.Entities;
using WrestleApplicationAPI.Interfaces;
using WrestleApplicationAPI.Models;
using WrestleApplicationAPI.Services;
using AutoMapper;

namespace WrestleApplicationAPI.Controllers
{
    [Route("api/continents/{continentId}/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly IMailService _mailService;
        private readonly IContinentRepository _continentRepository;
        private readonly IMapper _mapper;

        public CountriesController(ILogger<CountriesController> logger,
            IMailService mailService,
            IContinentRepository continentRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _continentRepository = continentRepository ?? throw new ArgumentNullException(nameof(continentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountries(int continentId)
        {
            if(!await _continentRepository.ContinentExistsAsync(continentId))
            {
                _logger.LogInformation($"Continent with id {continentId} wasn't found when accessing countries.");
                return NotFound();
            }
            var countriesForContinent = await _continentRepository.GetCountriesForContinentAsync(continentId);

            return Ok(_mapper.Map<IEnumerable<CountryDTO>>(countriesForContinent));
        }


        [HttpGet("{countryId}", Name = "GetCountry")]
        public async Task<ActionResult<CountryDTO>> GetCountry(int continentId, int countryId)
        {
            if(!await _continentRepository.ContinentExistsAsync(continentId))
            {
                return NotFound();
            }

            var country = await _continentRepository.GetCountryForContinentAsync(continentId, countryId);

            if (country == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CountryDTO>(country));
        }

        [HttpPost]
        public async Task<ActionResult<CountryDTO>> CreateCountry(int continentId, CountryCreationDTO country) 
        {
            if(!await _continentRepository.ContinentExistsAsync(continentId))
            {
                return NotFound(); 
            }
            
            var finalCountry = _mapper.Map<Entities.Country>(country);

            await _continentRepository.AddCountryForContinentAsync(continentId, finalCountry);

            await _continentRepository.SaveChangesAsync();

            var createdCountryToReturn = _mapper.Map<Models.CountryDTO>(finalCountry);

            return CreatedAtRoute("GetCountry",
                new
                {
                    continentId = continentId,
                    countryId = createdCountryToReturn.IdCountry,
                },
                createdCountryToReturn);
        }

        [HttpPut("{countryId}")]
        public async Task<ActionResult> ModificationCountry(int continentId, int countryId, CountryModificationDTO country)
        {
            if(!await _continentRepository.ContinentExistsAsync(continentId))
            {
                return NotFound("Continent not find"); 
            }

            var countryEntity = await _continentRepository.GetCountryForContinentAsync(continentId, countryId);

            if (countryEntity == null) 
            {
                return NotFound("Country not find");
            }

            _mapper.Map(country, countryEntity);

            await _continentRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{countryId}")]
        public async Task<ActionResult> PartialModificationCountry(int continentId, int countryId, JsonPatchDocument<CountryModificationDTO> patchDocument)
        {

            if (!await _continentRepository.ContinentExistsAsync(continentId))
            {
                return NotFound("Continent not find");
            }

            var countryEntity = await _continentRepository.GetCountryForContinentAsync(continentId, countryId);
            if (countryEntity == null) 
            { 
                return NotFound();
            }

            var countryToPatch = _mapper.Map<CountryModificationDTO>(countryEntity);

            patchDocument.ApplyTo(countryToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(countryToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(countryToPatch, countryEntity);
            await _continentRepository.SaveChangesAsync();

            return NoContent();
        }

        /*[HttpDelete("{countryid}")]
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
        }*/
    }
}
