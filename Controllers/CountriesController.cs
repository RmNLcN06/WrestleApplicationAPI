using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using WrestleApplicationAPI.Datas;
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
        public ActionResult<CountryDTO> CreateCountry(int continentId, CountryDTO country) 
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
    }
}
