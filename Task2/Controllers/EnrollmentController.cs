using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.Application.Dtos.DepartmentDtos;
using Task2.Application.Dtos.EnrollmentDtos;
using Task2.Application.Services.IServices;

namespace Task2.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentServices _erollmentservices;
        public EnrollmentController(IEnrollmentServices erollmentservices)
        {
            _erollmentservices = erollmentservices;
        }
        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _erollmentservices.GetById(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return Ok(result.Value);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _erollmentservices.GetAll();
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return Ok(result.Value);
        }
        //[ServiceFilter(typeof(ValidationFilter<DepartmentCreateDto>))]
        [HttpPost]

        public async Task<ActionResult> AddStudent([FromBody] EnrollmentCreateDto createDto)
        {
            var result = await _erollmentservices.AddEnrollment(createDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return NoContent();

        }
        [HttpDelete]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var result = await _erollmentservices.Delete(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return NoContent();

        }
        //[ServiceFilter(typeof(ValidationFilter<DepartmentUpdateDto>))]
        [HttpPost("UpdateEnrollment")]

        public async Task<ActionResult> UpdateStudent(int id, [FromBody] EnrollmentUpdateDto updateDto)
        {
            var result = await _erollmentservices.Update(id, updateDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error!.Message);
            return NoContent();
        }
    }
}
