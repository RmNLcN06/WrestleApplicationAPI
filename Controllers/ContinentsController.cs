﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WrestleApplicationAPI.DbContexts;
using WrestleApplicationAPI.Entities;
using WrestleApplicationAPI.Interfaces;
using WrestleApplicationAPI.Models;

namespace WrestleApplicationAPI.Controllers
{
    [Route("api/continents")]
    [ApiController]
    public class ContinentsController : ControllerBase
    {
        private readonly IContinentRepository _continentRepository;
        private readonly IMapper _mapper;

        public ContinentsController(IContinentRepository continentRepository, IMapper mapper)
        {
            _continentRepository = continentRepository ?? throw new ArgumentNullException(nameof(continentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContinentWithoutCountriesDTO>>> GetContinents()
        {
            var continentEntities = await _continentRepository.GetContinentsAsync();
            return Ok(_mapper.Map<IEnumerable<ContinentWithoutCountriesDTO>>(continentEntities));

            /*var results = new List <ContinentWithoutCountriesDTO>();
            foreach (var continentEntity in continentEntities) 
            {
                results.Add(new ContinentWithoutCountriesDTO
                {
                    IdContinent = continentEntity.IdContinent,
                    NameContinent = continentEntity.NameContinent
                });
            }

            return Ok(results);*/
            /*var continents = _continentsDataStore.Continents.ToList();
            return Ok(continents);*/
        }

        [HttpGet("{id}")]
        public ActionResult<ContinentDTO> GetContinentById(int id)
        {
            /*if (id <= 0)
            {
                return BadRequest("Wrong ID.");
            }

            var continent = _continentsDataStore.Continents.FirstOrDefault(c => c.IdContinent == id);
            if (continent == null)
            {
                return NotFound("Wrong Continent.");
            }
            
            return Ok(continent);*/
            return Ok();
        }
    }
}
