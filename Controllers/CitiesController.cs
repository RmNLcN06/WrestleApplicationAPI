using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WrestleApplicationAPI.Interfaces;
using WrestleApplicationAPI.Models.City;
using WrestleApplicationAPI.Repositories;
using WrestleApplicationAPI.Services;

namespace WrestleApplicationAPI.Controllers
{
    [Route("api/countries/{countryId}/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly IMailService _mailService;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CitiesController(ILogger<CountriesController> logger,
            IMailService mailService,
            ICountryRepository countryRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCities(int countryId)
        {
            if (!await _countryRepository.CountryExistsAsync(countryId))
            {
                _logger.LogInformation($"Country with id {countryId} wasn't found when accessing cities.");
                return NotFound();
            }
            var citiesForCountry = await _countryRepository.GetCitiesForCountryAsync(countryId);

            return Ok(_mapper.Map<IEnumerable<CityDTO>>(citiesForCountry));
        }


        [HttpGet("{cityId}", Name = "GetCity")]
        public async Task<ActionResult<CityDTO>> GetCity(int countryId, int cityId)
        {
            if (!await _countryRepository.CountryExistsAsync(countryId))
            {
                return NotFound();
            }

            var city = await _countryRepository.GetCityForCountryAsync(countryId, cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CityDTO>(city));
        }

        [HttpPost]
        public async Task<ActionResult<CityDTO>> CreateCity(int countryId, CityCreationDTO city)
        {
            if (!await _countryRepository.CountryExistsAsync(countryId))
            {
                return NotFound();
            }

            var finalCity = _mapper.Map<Entities.City>(city);

            await _countryRepository.AddCityForCountryAsync(countryId, finalCity);

            await _countryRepository.SaveChangesAsync();

            var createdCityToReturn = _mapper.Map<CityDTO>(finalCity);

            return CreatedAtRoute("GetCity",
                new
                {
                    countryId = countryId,
                    cityId = createdCityToReturn.IdCity,
                },
                createdCityToReturn);
        }

        [HttpPut("{cityId}")]
        public async Task<ActionResult> ModificationCity(int countryId, int cityId, CityModificationDTO city)
        {
            if (!await _countryRepository.CountryExistsAsync(countryId))
            {
                return NotFound("Country not find");
            }

            var cityEntity = await _countryRepository.GetCityForCountryAsync(countryId, cityId);

            if (cityEntity == null)
            {
                return NotFound("City not find");
            }

            _mapper.Map(city, cityEntity);

            await _countryRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{cityId}")]
        public async Task<ActionResult> PartialModificationCity(int countryId, int cityId, JsonPatchDocument<CityModificationDTO> patchDocument)
        {

            if (!await _countryRepository.CountryExistsAsync(countryId))
            {
                return NotFound("Country not find");
            }

            var cityEntity = await _countryRepository.GetCityForCountryAsync(countryId, cityId);
            if (cityEntity == null)
            {
                return NotFound();
            }

            var cityToPatch = _mapper.Map<CityModificationDTO>(cityEntity);

            patchDocument.ApplyTo(cityToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(cityToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(cityToPatch, cityEntity);
            await _countryRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{cityId}")]
        public async Task<ActionResult> DeleteCity(int countryId, int cityId)
        {
            if (!await _countryRepository.CountryExistsAsync(countryId))
            {
                return NotFound("Country not find");
            }

            var cityEntity = await _countryRepository.GetCityForCountryAsync(countryId, cityId);
            if (cityEntity == null)
            {
                return NotFound();
            }

            _countryRepository.DeleteCityForCountry(cityEntity);
            await _countryRepository.SaveChangesAsync();

            _mailService.Send("City deleted.", $"City {cityEntity.NameCity} with id {cityEntity.IdCity} was deleted.");

            return NoContent();
        }
    }
}
