using Microsoft.AspNetCore.Mvc;
using ServicesContract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController (IRegionService regionService)
        {
            _regionService = regionService;
        }

        // GET: api/<RegionController>
        [HttpGet]
        public async Task<IActionResult> GetRegions()
        {
            var regions = await _regionService.GetAllRegions();

            return Ok(regions);
        }
    }
}
