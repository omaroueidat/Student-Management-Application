using Microsoft.AspNetCore.Mvc;
using ServicesContract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeValuesController : ControllerBase
    {
        private readonly ICodeValueService _codeValueService;

        public CodeValuesController(ICodeValueService codeValueService)
        {
            _codeValueService = codeValueService;
        }

        // GET api/<CodeValuesController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var typeValue = await _codeValueService.GetCodeValues(id);

            return Ok(typeValue);
        }
    }
}
