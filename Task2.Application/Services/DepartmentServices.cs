using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.DepartmentDtos;
using Task2.Application.Dtos.StudentDtos;
using Task2.Application.Result;
using Task2.Application.Services.IServices;
using Task2.Domain.Interfaces;
using Task2.Domain.Models;

namespace Task2.Application.Services
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public DepartmentServices(IUniteOfWork uniteOfWork,
            IMapper mapper)
        {
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
        }

        public async Task<CustomResult<DepartmentCreateDto>> AddDepartment(DepartmentCreateDto CreateDto)
        {
            var departmentexist = await _uniteOfWork._DepartmentRepository.Get(s => s.Name == CreateDto.Name);
            if (departmentexist == null)
            {
                var department = _mapper.Map<Department>(CreateDto);
                await _uniteOfWork._DepartmentRepository.Add(department);
                var complete = await _uniteOfWork.Save();
                if (complete == 0)
                    return CustomResult<DepartmentCreateDto>.Failure(CustomError.NotFoundError("Faild in Save Department"));
                return CustomResult<DepartmentCreateDto>.Success(CreateDto);
            }
            return CustomResult<DepartmentCreateDto>.Failure(CustomError.ValidationError("Department with this name exist "));
        }

        public async Task<CustomResult> Delete(int id)
        {
            var departmentExist = await _uniteOfWork._DepartmentRepository.Get(s => s.Id == id);
            if (departmentExist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Student Not Found"));
            await _uniteOfWork._DepartmentRepository.Delete(departmentExist);
            var complete = await _uniteOfWork.Save();
            if (complete == 0)
                return CustomResult.Failure(CustomError.NotFoundError("Faild in Delete Department"));
            return CustomResult.Success();
        }

        public async  Task<CustomResult<List<DepartmentDto>>> GetAll()
        {
            var Departments = await _uniteOfWork._DepartmentRepository.GetAll();
            if (Departments == null)
            {
                return CustomResult<List<DepartmentDto>>.Failure(CustomError.NotFoundError("There is No Department"));
            }
            var DepartmentDto = _mapper.Map<List<DepartmentDto>>(Departments).ToList();
            return CustomResult<List<DepartmentDto>>.Success(DepartmentDto);
        }

        public async Task<CustomResult<DepartmentDto>> GetById(int id)
        {
            var department = await _uniteOfWork._DepartmentRepository.Get(s => s.Id == id);
            if (department == null)
                return CustomResult<DepartmentDto>.Failure(CustomError.NotFoundError("Department Not Found"));


            var departmentdto = _mapper.Map<DepartmentDto>(department);
            return CustomResult<DepartmentDto>.Success(departmentdto);
        }

        public async  Task<CustomResult> Update(int id, DepartmentUpdateDto UpdateDto)
        {
            var department = await _uniteOfWork._DepartmentRepository.Get(s => s.Id == id);
            if (department == null)
                return CustomResult.Failure(CustomError.NotFoundError("Department You Try To Update Not Found"));
            department = UpdateDto.Adapt(department);
            await _uniteOfWork._DepartmentRepository.Update(department);
            var complete = await _uniteOfWork.Save();
            if (complete == 0)
                return CustomResult.Failure(CustomError.NotFoundError("Faild in Update Department"));
            return CustomResult.Success();
        }
    }
}
