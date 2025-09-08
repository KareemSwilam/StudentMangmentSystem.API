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
    public class CourseController : ControllerBase
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ApiResponse _response;
        private readonly IMapper _mapper;
        public CourseController(IUniteOfWork uniteOfWork, IMapper mapper)
        {
            _response = new ApiResponse();
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
        }
        [HttpGet("GetByName")]
        public async Task<ActionResult<ApiResponse>> GetByName(string name)
        {
            if (name == null)
            {
                _response.ISuccussed = false;
                _response.Errors = new List<string> { "InVaild Input" };
                _response.StatusCodes = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var course = await _uniteOfWork._CourseRepository.Get(d => d.Title == name);
            if (course == null)
            {
                _response.ISuccussed = false;
                _response.Errors = new List<string> { "Department Not Found" };
                _response.StatusCodes = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.ISuccussed = true;
            _response.Result = course;
            _response.StatusCodes = HttpStatusCode.OK;
            return Ok(_response);
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
            var course = await _uniteOfWork._CourseRepository.Get(d => d.Id == id);
            if (course == null)
            {
                _response.ISuccussed = false;
                _response.Errors = new List<string> { "Department Not Found" };
                _response.StatusCodes = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.ISuccussed = true;
            _response.Result = course;
            _response.StatusCodes = HttpStatusCode.OK;
            return Ok(_response);

        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse>> GetAll()
        {

            var courses = await _uniteOfWork._CourseRepository.GetAll();
            if (courses == null)
            {
                _response.ISuccussed = false;
                _response.Errors = new List<string> { "Department Not Found" };
                _response.StatusCodes = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.ISuccussed = true;
            _response.Result = courses;
            _response.StatusCodes = HttpStatusCode.OK;
            return Ok(_response);

        }
        [HttpPost("AddCourse")]
        public async Task<ActionResult<ApiResponse>> AddCourse([FromBody] CourseUpdateDto createDto)
        {
            if (createDto != null && ModelState.IsValid)
            {
                var existdepartment = await _uniteOfWork._CourseRepository.Get(d => d.Title == createDto.Title);
                if (existdepartment != null)
                {
                    _response.ISuccussed = false;
                    _response.Errors = new List<string> { "Course Exist " };
                    _response.StatusCodes = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var course = _mapper.Map<Course>(createDto);
                await _uniteOfWork._CourseRepository.Add(course);
                var complete = await _uniteOfWork.Save();
                if (complete == 0)
                {
                    _response.ISuccussed = false;
                    _response.Errors = new List<string> { "Faild to Add Course" };
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
        [HttpGet("GetStudents")]
        public async Task<ActionResult<ApiResponse>> GetStudents(int id)
        {
            if(id > 0 )
            {
                var course = await _uniteOfWork._CourseRepository.Get( c=> c.Id == id );    
                if(course == null )
                {
                    _response.ISuccussed = false;
                    _response.StatusCodes = HttpStatusCode.NotFound;
                    _response.Errors = new List<string> { "Course Not Found" };
                    return NotFound(_response);
                }
                var students = await _uniteOfWork._CourseRepository.GetStudents(id); ;
                if(students == null )
                {
                    _response.ISuccussed = false;
                    _response.StatusCodes = HttpStatusCode.NotFound;
                    _response.Errors = new List<string> { "There is No Student Asign to this course" };
                    return NotFound(_response);
                }
                _response.ISuccussed = true;
                _response.Result = students;
                _response.StatusCodes = HttpStatusCode.OK;
                return Ok(_response);
            }
            _response.ISuccussed = false ;
            _response.Errors = new List<string> { "InVaild Input" };
            _response.StatusCodes= HttpStatusCode.BadRequest;
            return BadRequest(_response);
        }
    }
}
