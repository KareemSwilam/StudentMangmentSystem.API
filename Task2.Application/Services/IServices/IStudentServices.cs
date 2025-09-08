using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.StudentDtos;
using Task2.Application.Result;

namespace Task2.Application.Services.IServices
{
    public interface IStudentServices
    {
        public Task<CustomResult<StudentDto>> GetById(int id);
        public Task<CustomResult<List<StudentDto>>> GetAll();
        public Task<CustomResult<StudentCreateDto>> AddStudent(StudentCreateDto CreateDto);
        public Task<CustomResult> Delete(int id);
        public Task<CustomResult> Update(int id , StudentUpdateDto UpdateDto);
    }
}
