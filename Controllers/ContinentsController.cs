using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Text.Json;
using WrestleApplicationAPI.DbContexts;
using WrestleApplicationAPI.Entities;
using WrestleApplicationAPI.Interfaces;
using WrestleApplicationAPI.Models.Continent;
using WrestleApplicationAPI.Models.Country;

namespace WrestleApplicationAPI.Controllers
{
    [Route("api/continents")]
    [ApiController]
    public class ContinentsController : ControllerBase
    {
        private readonly IContinentRepository _continentRepository;
        private readonly IMapper _mapper;
        const int maxContinentsPageSize = 20;

        public ContinentsController(IContinentRepository continentRepository, IMapper mapper)
        {
            _continentRepository = continentRepository ?? throw new ArgumentNullException(nameof(continentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContinentWithoutCountriesDTO>>> GetContinents(string? nameContinent, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxContinentsPageSize) 
            { 
                pageSize = maxContinentsPageSize;
            }

            var (continentEntities, paginationMetadata) = await _continentRepository.GetContinentsAsync(nameContinent, searchQuery, pageNumber, pageSize);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<ContinentWithoutCountriesDTO>>(continentEntities));
        }

        [HttpGet("{continentId}", Name = "GetContinent")]
        public async Task<IActionResult> GetContinent(int continentId, bool includeCountries = false)
        {
            var continent = await _continentRepository.GetContinentAsync(continentId, includeCountries);

            if (continent == null) 
            { 
                return NotFound();
            }

            if (includeCountries) 
            { 
                return Ok(_mapper.Map<ContinentDTO>(continent));
            }
           
            return Ok(_mapper.Map<ContinentWithoutCountriesDTO>(continent));
        }

        [HttpPost]
        public async Task<ActionResult<ContinentCreationDTO>> AddContinent(Continent continent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var finalContinent = _mapper.Map<Entities.Continent>(continent);

            await _continentRepository.AddContinentAsync(finalContinent);

            await _continentRepository.SaveChangesAsync();

            var createdContinentToReturn = _mapper.Map<ContinentDTO>(finalContinent);

            return CreatedAtRoute("GetContinent",
                new
                {
                    continentId = createdContinentToReturn.IdContinent,
                },
                createdContinentToReturn);
        }

        [HttpPut("{continentId}")]
        public async Task<ActionResult> ModificationContinent(int continentId, ContinentModificationDTO continent, bool includeCountries = false)
        {
            if (!await _continentRepository.ContinentExistsAsync(continentId))
            {
                return NotFound("Continent not find");
            }

            var continentEntity = await _continentRepository.GetContinentAsync(continentId, includeCountries);

            if (continentEntity == null)
            {
                return NotFound("Continent not find");
            }

            _mapper.Map(continent, continentEntity);

            await _continentRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
