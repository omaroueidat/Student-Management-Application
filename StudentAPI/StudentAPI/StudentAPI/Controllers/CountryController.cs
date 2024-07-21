using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public CountryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCountry(Guid id)
        {
            var country = await _context.Countries.SingleOrDefaultAsync(c => c.CountryId == id);

            if (country is null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpGet("Regions/{id:guid}")]
        public async Task<IActionResult> GetCountryWithRegions(Guid id)
        {
            var country = await _context.Countries
                .SingleOrDefaultAsync(c => c.CountryId == id);

            if (country is null)
            {
                return NotFound();
            }

            return Ok(country);
        }


    }
}
