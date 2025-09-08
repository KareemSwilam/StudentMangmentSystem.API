using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.CourseDtos;
using Task2.Application.Dtos.DepartmentDtos;
using Task2.Application.Result;

namespace Task2.Application.Services.IServices
{
    public interface ICourseServices
    {
        public Task<CustomResult<CourseDto>> GetById(int id);
        public Task<CustomResult<List<CourseDto>>> GetAll();
        public Task<CustomResult<CourseCreateDto>> AddCourse(CourseCreateDto CreateDto);
        public Task<CustomResult> Delete(int id);
        public Task<CustomResult> Update(int id, CourseUpdateDto UpdateDto);
    }
}
