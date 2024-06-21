﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WrestleApplicationAPI.Datas;
using WrestleApplicationAPI.Entities;
using WrestleApplicationAPI.Models;

namespace WrestleApplicationAPI.Controllers
{
    [Route("api/continents")]
    [ApiController]
    public class ContinentsController : ControllerBase
    {
        private readonly ContinentsDataStore _continentsDataStore;

        /*private readonly DataContext _context;

        public ContinentsController(DataContext context)
        {
        _context = context;
        }*/

        public ContinentsController(ContinentsDataStore continentsDataStore) 
        {
            _continentsDataStore = continentsDataStore ?? throw new ArgumentNullException(nameof(continentsDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContinentDTO>> GetContinents()
        {
            var continents = _continentsDataStore.Continents.ToList();
            return Ok(continents);
        }

        [HttpGet("{id}")]
        public ActionResult<ContinentDTO> GetContinentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Wrong ID.");
            }

            var continent = _continentsDataStore.Continents.FirstOrDefault(c => c.IdContinent == id);
            if (continent == null)
            {
                return NotFound("Wrong Continent.");
            }
            
            return Ok(continent);
        }
    }
}
