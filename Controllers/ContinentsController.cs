using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WrestleApplicationAPI.Datas;
using WrestleApplicationAPI.Entities;

namespace WrestleApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContinentsController : ControllerBase
    {
        private readonly DataContext _context;

        public ContinentsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Continent>>> GetContinents()
        {
            return await _context.Continents.ToListAsync();
        }
    }
}
