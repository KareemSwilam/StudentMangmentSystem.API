using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.DepartmentDtos;
using Task2.Application.Dtos.StudentDtos;
using Task2.Application.Result;

namespace Task2.Application.Services.IServices
{
    public interface IDepartmentServices
    {
        public Task<CustomResult<DepartmentDto>> GetById(int id);
        public Task<CustomResult<List<DepartmentDto>>> GetAll();
        public Task<CustomResult<DepartmentCreateDto>> AddDepartment(DepartmentCreateDto CreateDto);
        public Task<CustomResult> Delete(int id);
        public Task<CustomResult> Update(int id, DepartmentUpdateDto UpdateDto);
    }
}
