using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Task2.Core.IRepository;
using Task2.Core.Models;
using Task2.Core.Models.Dtos;

namespace Task2.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ApiResponse _response;
        private readonly IMapper _mapper;
        public StudentController(IUniteOfWork uniteOfWork , IMapper mapper)
        {
            _uniteOfWork = uniteOfWork;
            _response = new ApiResponse();
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetById(int id)
        {
            if (id <= 0)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.BadRequest;
                _response.Errors = new List<string> { "InVaild Input" };
            }
            var student = await _uniteOfWork._StudentRepository.Get(s => s.Id == id); 
            if (student == null)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "Student Not Exist" };
            }
            _response.ISuccussed = true;
            _response.StatusCodes = HttpStatusCode.OK;
            _response.Result = student;
            return Ok(_response);
        }
        [HttpGet("GetByName")]
        public async Task<ActionResult<ApiResponse>> GetByName(string name)
        {
            if (name == null)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.BadRequest;
                _response.Errors = new List<string> { "InVaild Input" };
            }
            var student = await _uniteOfWork._StudentRepository.Get(s => s.Name.ToLower() == name.ToLower());
            if (student == null)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "Student Not Exist" };
            }
            _response.ISuccussed = true;
            _response.StatusCodes = HttpStatusCode.OK;
            _response.Result = student;
            return Ok(_response);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse>> GetAll()
        {
            var student = await _uniteOfWork._StudentRepository.GetAll();
            if (student == null)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "Students Not Exist" };
            }
            _response.ISuccussed = true;
            _response.StatusCodes = HttpStatusCode.OK;
            _response.Result = student;
            return Ok(_response);
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddStudent([FromBody] StudentCreateDto studentDto)
        {
            if (studentDto != null && ModelState.IsValid)
            {
                var checkstudent = await _uniteOfWork._StudentRepository.Get(s => s.Email.ToLower() == studentDto.Email.ToLower());
                if (checkstudent != null)
                {
                    _response.ISuccussed = false;
                    _response.StatusCodes = HttpStatusCode.BadRequest;
                    _response.Errors = new List<string> { "Email Exist" };
                    return BadRequest(_response);
                }
                var student = _mapper.Map<Student>(studentDto);
                await _uniteOfWork._StudentRepository.Add(student);
                if (await _uniteOfWork.Save() == 0)
                {
                    _response.ISuccussed = false;
                    _response.StatusCodes = HttpStatusCode.InternalServerError;
                    _response.Errors = new List<string> { "Faild in Save Student" };
                    return BadRequest(_response);
                }
                _response.ISuccussed = true;
                _response.StatusCodes = HttpStatusCode.Created;

                return Ok(_response);
                
            }
            _response.ISuccussed = false;
            _response.StatusCodes = HttpStatusCode.BadRequest;
            _response.Errors = new List<string> { "InVaild Input" };
            return BadRequest(_response);

        }
        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> DelateStudent(int id)
        {
            if (id <= 0)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.BadRequest;
                _response.Errors = new List<string> { "InVaild Input" };
            }
            var student = await _uniteOfWork._StudentRepository.Get(s => s.Id == id);
            if (student == null)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "Student Not Exist" };
            }
            await _uniteOfWork._StudentRepository.Delete(student);
            var complete = await _uniteOfWork.Save();
            if(complete == 0)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.InternalServerError;
                _response.Errors = new List<string> { "Faild in Delete Student" };
                return BadRequest(_response);
            }
            _response.ISuccussed = true;
            _response.StatusCodes = HttpStatusCode.NoContent;

            return Ok(_response);
        }
        [HttpPost("UpdateStudent")]
        public async Task<ActionResult<ApiResponse>> Update(int id , [FromBody] StudentUpdateDto studentdto)
        {
            if (id <= 0)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.BadRequest;
                _response.Errors = new List<string> { "InVaild Input" };
            }
            var student = await _uniteOfWork._StudentRepository.Get(s => s.Id == id , false);
            if (student == null)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "Student Not Exist" };
            }
            student = studentdto.Adapt(student);
            await _uniteOfWork._StudentRepository.Update(student);
            var complete = await _uniteOfWork.Save();
            if (complete == 0)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.InternalServerError;
                _response.Errors = new List<string> { "Faild in Update Student" };
                return BadRequest(_response);
            }
            _response.ISuccussed = true;
            _response.StatusCodes = HttpStatusCode.NoContent;

            return Ok(_response);
        }
        [HttpGet("GetCourses")]
        public async Task<ActionResult<ApiResponse>> GetCourses(int id)
        {
            if (id > 0)
            {
                var student = await _uniteOfWork._StudentRepository.Get(s => s.Id == id);
                if (student == null)
                {
                    _response.ISuccussed = false;
                    _response.Errors = new List<string> { "Student Not Found" };
                    _response.StatusCodes = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                var courses = await _uniteOfWork._StudentRepository.GetCourses(id);
                if (courses == null)
                {
                    _response.ISuccussed = false;
                    _response.StatusCodes = HttpStatusCode.NotFound;
                    _response.Errors = new List<string> { "The student Does't asign to any course" };
                    return NotFound(_response);
                }
                _response.ISuccussed = true;
                _response.StatusCodes = HttpStatusCode.OK;
                _response.Result = courses;
                return Ok(_response);
            }
            _response.ISuccussed = false;
            _response.StatusCodes= HttpStatusCode.BadRequest;
            _response.Errors = new List<string> { "InVaild InPut" };
            return BadRequest(_response);
        }

    }
}
