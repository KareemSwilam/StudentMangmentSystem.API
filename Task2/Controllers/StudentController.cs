using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Task2.Application.Dtos.StudentDtos;
using Task2.Application.Services.IServices;
using Task2.Application.Validations;

namespace Task2.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentservices;
        public StudentController(IStudentServices studentservices)
        {
            _studentservices = studentservices;
        }
        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _studentservices.GetById(id);
            if(!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return Ok(result.Value);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetById()
        {
            var result = await _studentservices.GetAll();
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return Ok(result.Value);
        }
        
        [HttpDelete]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var result = await _studentservices.Delete(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return NoContent();

        }
        [ServiceFilter(typeof(ValidationFilter<StudentCreateDto>))]
        [HttpPost("AddStudent")]
        public async Task<ActionResult> AddStudent([FromBody] StudentCreateDto studentCreateDto)
        {
            var result = await _studentservices.AddStudent(studentCreateDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return NoContent();
        }
        [HttpPost("UpdateStudent")]
        public async Task<ActionResult> UpdateStudent(int id , [FromBody] StudentUpdateDto studentUpdateDto)
        {
            var result = await _studentservices.Update(id, studentUpdateDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return NoContent();
        }
        
    }
}
