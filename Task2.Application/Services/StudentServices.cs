using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.StudentDtos;
using Task2.Application.Result;
using Task2.Application.Services.IServices;
using Task2.Domain.Interfaces;
using Task2.Domain.Models;

namespace Task2.Application.Services
{
    public class StudentServices: IStudentServices
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public StudentServices(IUniteOfWork uniteOfWork,
            IMapper mapper)
        {
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
        }

        public async Task<CustomResult<StudentCreateDto>> AddStudent(StudentCreateDto CreateDto)
        {
            var studentexist = await _uniteOfWork._StudentRepository.Get(s => s.Email == CreateDto.Email);
            if (studentexist == null)
            {
                var student = _mapper.Map<Student>(CreateDto);
                await _uniteOfWork._StudentRepository.Add(student);
                var complete = await _uniteOfWork.Save();
                if (complete == 0)
                    return CustomResult<StudentCreateDto>.Failure(CustomError.NotFoundError("Faild in Save Student"));
                return CustomResult<StudentCreateDto>.Success(CreateDto);
            }
            return CustomResult<StudentCreateDto>.Failure(StudentError.AlreadyExist(CreateDto.Email));

        }

        public async Task<CustomResult> Delete(int id)
        { 
            var studentExist = await _uniteOfWork._StudentRepository.Get(s => s.Id == id);
            if (studentExist == null) 
                return CustomResult.Failure(CustomError.NotFoundError("Student Not Found"));
            await _uniteOfWork._StudentRepository.Delete(studentExist);
            var complete = await _uniteOfWork.Save();
            if (complete == 0)
                return CustomResult.Failure(CustomError.NotFoundError("Faild in Delete Student"));
            return CustomResult.Success();


        }

        public async Task<CustomResult<List<StudentDto>>> GetAll()
        {
            var students = await _uniteOfWork._StudentRepository.GetAll();
            if(students == null)
            {
                return CustomResult<List<StudentDto>>.Failure(CustomError.NotFoundError("There is No Student"));
            }
            var StudentsDto = _mapper.Map<List<StudentDto>>(students).ToList();
            return CustomResult<List<StudentDto>>.Success(StudentsDto);
        }

        public async Task<CustomResult<StudentDto>> GetById(int id)
        {
            var student = await _uniteOfWork._StudentRepository.Get(s => s.Id == id);
            if(student == null)
                return CustomResult<StudentDto>.Failure(CustomError.NotFoundError("Strudent Not Found"));
                
            
            var studentDto = _mapper.Map<StudentDto>(student);
            return CustomResult<StudentDto>.Success(studentDto);

        }

        public async  Task<CustomResult> Update(int id , StudentUpdateDto UpdateDto)
        {
            var student = await _uniteOfWork._StudentRepository.Get(s => s.Id == id);
            if (student == null)
                return CustomResult.Failure(CustomError.NotFoundError("Strudent You Try To Update Not Found"));
            student = UpdateDto.Adapt(student);
            await _uniteOfWork._StudentRepository.Update(student);
            var complete = await _uniteOfWork.Save();
            if (complete == 0)
                return CustomResult.Failure(CustomError.NotFoundError("Faild in Update Student"));
            return CustomResult.Success();

        }
    }
}
