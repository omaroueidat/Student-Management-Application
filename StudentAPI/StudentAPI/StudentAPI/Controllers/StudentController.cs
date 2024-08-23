using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;
using ServicesContract;
using ServicesContract.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IStudentService _studentService;

        public StudentController(AppDbContext context, IStudentService studentService)
        {
            _context = context;
            _studentService = studentService;
        }

        // GET: api/<StudentController>?searchBy=""&searchString=""
        [HttpGet]
        public async Task<IActionResult> Get(string? searchBy, string? searchString)
        {
            var students = await _studentService.FiterStudents(searchBy, searchString);

            return Ok(students);
        }


        // GET api/<StudentController>/5
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var student = await _studentService.GetStudent(id);

            if (student is null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<IActionResult> Post(StudentRequest student)
        {
            var studentAdded = await _studentService.AddNewStudent(student);

            return CreatedAtAction("Get", new { id = studentAdded.StudentId });
        }

        // PUT api/<StudentController>
        [HttpPut("{studentId:guid}")]
        public async Task<IActionResult> Update(StudentUpdateRequest student, Guid studentId)
        {
            try
            {
                await _studentService.UpdateStudent(student, studentId);
            } 
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            bool isDeleted = await _studentService.DeleteStudent(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
