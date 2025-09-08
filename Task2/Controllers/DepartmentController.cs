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
    public class DepartmentController : ControllerBase
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly ApiResponse _response;
        private readonly IMapper _mapper;
        public DepartmentController(IUniteOfWork uniteOfWork, IMapper mapper)
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
            var department = await _uniteOfWork._DepartmentRepository.Get(d => d.Name == name);
            if (department == null)
            {
                _response.ISuccussed = false;
                _response.Errors = new List<string> { "Department Not Found" };
                _response.StatusCodes = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.ISuccussed = true;
            _response.Result = department;
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
            var department = await _uniteOfWork._DepartmentRepository.Get(d => d.Id == id);
            if (department == null)
            {
                _response.ISuccussed = false;
                _response.Errors = new List<string> { "Department Not Found" };
                _response.StatusCodes = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.ISuccussed = true;
            _response.Result = department;
            _response.StatusCodes = HttpStatusCode.OK;
            return Ok(_response);

        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse>> GetAll()
        {

            var department = await _uniteOfWork._DepartmentRepository.GetAll();
            if (department == null)
            {
                _response.ISuccussed = false;
                _response.Errors = new List<string> { "Department Not Found" };
                _response.StatusCodes = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.ISuccussed = true;
            _response.Result = department;
            _response.StatusCodes = HttpStatusCode.OK;
            return Ok(_response);

        }
        [HttpPost("AddDepartment")]
        public async Task<ActionResult<ApiResponse>> AddDepartment([FromBody] DepartmentCreateDto createDto)
        {
            if (createDto != null && ModelState.IsValid)
            {
                var existdepartment = await _uniteOfWork._DepartmentRepository.Get(d => d.Name == createDto.Name);
                if (existdepartment != null)
                {
                    _response.ISuccussed = false;
                    _response.Errors = new List<string> { "Department Exist " };
                    _response.StatusCodes = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var department = _mapper.Map<Department>(createDto);
                await _uniteOfWork._DepartmentRepository.Add(department);
                var complete = await _uniteOfWork.Save();
                if (complete == 0)
                {
                    _response.ISuccussed = false;
                    _response.Errors = new List<string> { "Faild to Add Department" };
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
        public async Task<ActionResult<ApiResponse>> DeleteDepartment(int id)
        {
            if (id <= 0)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.BadRequest;
                _response.Errors = new List<string> { "InVaild Input" };
                return BadRequest(_response);
            }
            var existdepartment = await _uniteOfWork._DepartmentRepository.Get(d => d.Id == id);
            if (existdepartment == null)
            {
                _response.ISuccussed = false;
                _response.StatusCodes = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "Department Not Found" };
                return NotFound(_response);
            }
            await _uniteOfWork._DepartmentRepository.Delete(existdepartment);
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
        public async Task<ActionResult<ApiResponse>> UpdateDepartment(int id , [FromBody] DepartmentUpdateDto departmentUpdateDto)
        {
            if( id > 0  && departmentUpdateDto != null)
            {
                var departmnet = await _uniteOfWork._DepartmentRepository.Get(d => d.Id == id); 
                if( departmnet == null )
                {
                    _response.ISuccussed = false ;
                    _response.Errors = new List<string> { "Department Your Try To Update NotFound" };
                    _response.StatusCodes= HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                departmnet = departmentUpdateDto.Adapt(departmnet);
                await _uniteOfWork._DepartmentRepository.Update(departmnet);
                var compelete = await _uniteOfWork.Save();
                if(compelete == 0 )
                {
                    _response.ISuccussed = false;
                    _response.Errors = new List<string> { "Faild to Update Department" };
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
        [HttpGet("GetCourses")]
        public async Task<ActionResult<ApiResponse>> GetCourses(int id)
        {
            if (id > 0)
            {
                var department = await _uniteOfWork._DepartmentRepository.GetCourses(id);
                if (department == null)
                {
                    _response.ISuccussed = false;
                    _response.Errors = new List<string> { "Department Not Found" };
                    _response.StatusCodes = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.ISuccussed = true;
                _response.StatusCodes= HttpStatusCode.OK;
                _response.Result = department.Courses;
                return Ok(_response);
            }
            _response.ISuccussed = false;
            _response.Errors = new List<string> { "InVaild Input" };
            _response.StatusCodes = HttpStatusCode.BadRequest;
            return BadRequest(_response);
        }
    }
}
