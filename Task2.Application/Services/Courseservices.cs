using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.CourseDtos;
using Task2.Application.Dtos.DepartmentDtos;
using Task2.Application.Result;
using Task2.Application.Services.IServices;
using Task2.Domain.Interfaces;
using Task2.Domain.Models;

namespace Task2.Application.Services
{
    public class Courseservices:ICourseServices
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public Courseservices(IUniteOfWork uniteOfWork,
            IMapper mapper)
        {
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
        }

        public async Task<CustomResult<CourseCreateDto>> AddCourse(CourseCreateDto CreateDto)
        {
            var Courseexist = await _uniteOfWork._CourseRepository.Get(s => s.Title == CreateDto.Title);
            if (Courseexist == null)
            {
                var departmentexist = await _uniteOfWork._DepartmentRepository.Get(d => d.Id == CreateDto.DepartmentId);
                if (departmentexist == null)
                    return CustomResult<CourseCreateDto>.Failure(CustomError.NotFoundError("Department you try to asign Course to it Not Found"));
                var course = _mapper.Map<Course>(CreateDto);
                await _uniteOfWork._CourseRepository.Add(course);
                var complete = await _uniteOfWork.Save();
                if (complete == 0)
                    return CustomResult<CourseCreateDto>.Failure(CustomError.NotFoundError("Faild in Save Course"));
                return CustomResult<CourseCreateDto>.Success(CreateDto);
            }
            return CustomResult<CourseCreateDto>.Failure(CustomError.ValidationError("Course with this name exist "));
        }

        public async Task<CustomResult> Delete(int id)
        {
            var Courseexist = await _uniteOfWork._CourseRepository.Get(s => s.Id == id);
            if (Courseexist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Course Not Found"));
            await _uniteOfWork._CourseRepository.Delete(Courseexist);
            var complete = await _uniteOfWork.Save();
            if (complete == 0)
                return CustomResult.Failure(CustomError.NotFoundError("Faild in Delete Department"));
            return CustomResult.Success();
        }

        public async Task<CustomResult<List<CourseDto>>> GetAll()
        {
            var Courses = await _uniteOfWork._CourseRepository.GetAll();
            if (Courses == null)
            {
                return CustomResult<List<CourseDto>>.Failure(CustomError.NotFoundError("There is No Courses"));
            }
            var CoursesDto = _mapper.Map<List<CourseDto>>(Courses).ToList();
            return CustomResult< List < CourseDto >>.Success(CoursesDto);
        }

        public async Task<CustomResult<CourseDto>> GetById(int id)
        {
            var course = await _uniteOfWork._CourseRepository.Get(s => s.Id == id);
            if (course == null)
                return CustomResult<CourseDto>.Failure(CustomError.NotFoundError("Course Not Found"));


            var courseDto = _mapper.Map<CourseDto>(course);
            return CustomResult<CourseDto>.Success(courseDto);
        }

        public async Task<CustomResult> Update(int id, CourseUpdateDto UpdateDto)
        {
            var course = await _uniteOfWork._CourseRepository.Get(s => s.Id == id);
            if (course == null)
                return CustomResult.Failure(CustomError.NotFoundError("Course You Try To Update Not Found"));
            course = UpdateDto.Adapt(course);
            await _uniteOfWork._CourseRepository.Update(course);
            var complete = await _uniteOfWork.Save();
            if (complete == 0)
                return CustomResult.Failure(CustomError.NotFoundError("Faild in Update Department"));
            return CustomResult.Success();
        }
    }
}
