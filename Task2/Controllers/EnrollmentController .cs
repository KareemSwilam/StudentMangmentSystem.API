using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Task2.Core;
using Task2.Core.IRepository;
using Task2.Core.Models;
using Task2.Core.Models.Dtos;

namespace Task2.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ApiResponse _response;
        private readonly IMapper _mapper;
        public EnrollmentController(IUniteOfWork uniteOfWork, IMapper mapper)
        {
            _response = new ApiResponse();
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
        }
       
        [HttpGet("GetById")]
        public async Task<ActionResult<ApiResponse>> GetById(int id)
        {
            if (id <= 0)
            {
                _response.ISuccussed = false;
                _response.Errors = new List<string> { "InVaild Input" };
                _response.StatusCodes = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var enrollment = await _uniteOfWork._EnrollmentRepository.Get(d => d.Id == id);
            if (enrollment == null)
            {
                _response.ISuccussed = false;
                _response.Errors = new List<string> { "Enrollment Not Found" };
                _response.StatusCodes = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.ISuccussed = true;
            _response.Result = enrollment;
            _response.StatusCodes = HttpStatusCode.OK;
            return Ok(_response);

        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse>> GetAll()
        {

            var enrollment = await _uniteOfWork._EnrollmentRepository.GetAll();
            if (enrollment == null)
            {
                _response.ISuccussed = false;
                _response.Errors = new List<string> { "enrollments Not Found" };
                _response.StatusCodes = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.ISuccussed = true;
            _response.Result = enrollment;
            _response.StatusCodes = HttpStatusCode.OK;
            return Ok(_response);

        }
        [HttpPost("AddEnrollment")]
        public async Task<ActionResult<ApiResponse>> AddEnrollment([FromBody] EnrollmentUpdateDto createDto)
        {
            if (createDto != null && ModelState.IsValid)
            {
                var existenrollment = await _uniteOfWork._EnrollmentRepository.Get(d => (d.StudentID == createDto.StudentID) && (d.CourseID == createDto.CourseID));
                if (existenrollment != null)
                {
                    _response.ISuccussed = false;
                    _response.Errors = new List<string> { "This Enrollment Exist " };
                    _response.StatusCodes = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var enrollment = _mapper.Map<Enrollment>(createDto);
                await _uniteOfWork._EnrollmentRepository.Add(enrollment);
                var complete = await _uniteOfWork.Save();
                if (complete == 0)
                {
                    _response.ISuccussed = false;
                    _response.Errors = new List<string> { "Faild to Add Enrollment" };
                    _response.StatusCodes = HttpStatusCode.InternalServerError;
                    return BadRequest(_response);
                }
                _response.ISuccussed = true;
                _response.StatusCodes = HttpStatusCode.Created;
                return Ok(_response);
            }
            _response.ISuccussed = false;
            _response.Errors = new List<string> { "InVaild Input " };
            _response.StatusCodes = HttpStatusCode.BadRequest;
            return BadRequest(_response);
        }
        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> DeleteCourse(int id)
        {
            if (id <= 0)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.BadRequest;
                _response.Errors = new List<string> { "InVaild Input" };
                return BadRequest(_response);
            }
            var existcourse = await _uniteOfWork._CourseRepository.Get(d => d.Id == id);
            if (existcourse == null)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "Department Not Found" };
                return NotFound(_response);
            }
            await _uniteOfWork._CourseRepository.Delete(existcourse);
            var complete = await _uniteOfWork.Save();
            if (complete == 0)
            {
                _response.ISuccussed = false;
                _response.Errors = new List<string> { "Faild to Add Department" };
                _response.StatusCodes = HttpStatusCode.InternalServerError;
                return BadRequest(_response);
            }
            _response.ISuccussed = true;
            _response.StatusCodes = HttpStatusCode.NoContent;
            return Ok(_response);
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> UpdateCourse(int id , [FromBody] CourseUpdateDto courseUpdateDto)
        {
            if( id > 0  && courseUpdateDto != null)
            {
                var course = await _uniteOfWork._CourseRepository.Get(d => d.Id == id); 
                if(course == null )
                {
                    _response.ISuccussed = false ;
                    _response.Errors = new List<string> { "Course Your Try To Update NotFound" };
                    _response.StatusCodes= HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                course = courseUpdateDto.Adapt(course);
                await _uniteOfWork._CourseRepository.Update(course);
                var compelete = await _uniteOfWork.Save();
                if(compelete == 0 )
                {
                    _response.ISuccussed = false;
                    _response.Errors = new List<string> { "Faild to Update Course" };
                    _response.StatusCodes = HttpStatusCode.InternalServerError;
                    return BadRequest(_response);
                }
                _response.ISuccussed = true;
                _response.StatusCodes = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            _response.ISuccussed = false;
            _response.Errors = new List<string> { "InVaild Input " };
            _response.StatusCodes = HttpStatusCode.BadRequest;
            return BadRequest(_response);
        }
    }
}
