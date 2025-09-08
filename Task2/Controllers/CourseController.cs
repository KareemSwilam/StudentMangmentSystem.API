using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.Application.Dtos.CourseDtos;
using Task2.Application.Dtos.DepartmentDtos;
using Task2.Application.Services;
using Task2.Application.Services.IServices;

namespace Task2.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseServices _courseservices;
        public CourseController(ICourseServices courseservices)
        {
            _courseservices = courseservices;
        }
        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _courseservices.GetById(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return Ok(result.Value);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetById()
        {
            var result = await _courseservices.GetAll();
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return Ok(result.Value);
        }
        //[ServiceFilter(typeof(ValidationFilter<DepartmentCreateDto>))]
        [HttpPost]

        public async Task<ActionResult> AddStudent([FromBody] CourseCreateDto createDto)
        {
            var result = await _courseservices.AddCourse(createDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return NoContent();

        }
        [HttpDelete]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var result = await _courseservices.Delete(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return NoContent();

        }
        //[ServiceFilter(typeof(ValidationFilter<DepartmentUpdateDto>))]
        [HttpPost("UpdateCourse")]

        public async Task<ActionResult> UpdateStudent(int id, [FromBody] CourseUpdateDto updateDto)
        {
            var result = await _courseservices.Update(id, updateDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return NoContent();
        }
    }
}
