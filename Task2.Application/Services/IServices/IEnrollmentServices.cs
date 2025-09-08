using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task2.Application.Dtos.EnrollmentDtos;
using Task2.Application.Result;

namespace Task2.Application.Services.IServices
{
    public interface IEnrollmentServices
    {
        public Task<CustomResult<EnrollmentDto>> GetById(int id);
        public Task<CustomResult<List<EnrollmentDto>>> GetAll();
        public Task<CustomResult<EnrollmentCreateDto>> AddEnrollment(EnrollmentCreateDto CreateDto);
        public Task<CustomResult> Delete(int id);
        public Task<CustomResult> Update(int id, EnrollmentUpdateDto UpdateDto);
    }
}
