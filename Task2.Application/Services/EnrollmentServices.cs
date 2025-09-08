using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.CourseDtos;
using Task2.Application.Dtos.EnrollmentDtos;
using Task2.Application.Result;
using Task2.Application.Services.IServices;
using Task2.Domain.Interfaces;
using Task2.Domain.Models;

namespace Task2.Application.Services
{
    public class EnrollmentServices : IEnrollmentServices
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public EnrollmentServices(IUniteOfWork uniteOfWork,
            IMapper mapper)
        {
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
        }
        public async Task<CustomResult<EnrollmentCreateDto>> AddEnrollment(EnrollmentCreateDto CreateDto)
        {
            var studentExist = await _uniteOfWork._StudentRepository.Get(s => s.Id == CreateDto.StudentID); ;
            if (studentExist == null)
                return CustomResult<EnrollmentCreateDto>.Failure(CustomError.NotFoundError("Student You try To Add Not Found"));
            var courseExist =  await _uniteOfWork._CourseRepository.Get(c => c.Id == CreateDto.CourseID);
            if (studentExist == null)
                return CustomResult<EnrollmentCreateDto>.Failure(CustomError.NotFoundError("Course You try To sign into Not Found"));
            var enrollment = _mapper.Map<Enrollment>(CreateDto);
            await _uniteOfWork._EnrollmentRepository.Add(enrollment);
            var complete = await _uniteOfWork.Save();
            if (complete == 0)
                return CustomResult<EnrollmentCreateDto>.Failure(CustomError.NotFoundError("Faild in Save Enrollment"));
            return CustomResult<EnrollmentCreateDto>.Success(CreateDto);
        }

        public async Task<CustomResult> Delete(int id)
        {
            var enrollmentexist = await _uniteOfWork._EnrollmentRepository.Get(e => e.Id == id);
            if (enrollmentexist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Enrollment you try to delete not found"));
            await _uniteOfWork._EnrollmentRepository.Delete(enrollmentexist);
            var complete = await _uniteOfWork.Save();
            if (complete == 0)
                return CustomResult.Failure(CustomError.NotFoundError("Faild in Delete Enrollment"));
            return CustomResult.Success();

        }

        public async Task<CustomResult<List<EnrollmentDto>>> GetAll()
        {
            var enrollments = await _uniteOfWork._EnrollmentRepository.GetAllWithStudentAndCourses();
            if(enrollments == null)
                return CustomResult<List<EnrollmentDto>>.Failure(CustomError.NotFoundError("Enrollments Not Found"));
            var enrollmentsdto = _mapper.Map<List<EnrollmentDto>>(enrollments);
            return CustomResult<List<EnrollmentDto>>.Success(enrollmentsdto);

        }

        public async  Task<CustomResult<EnrollmentDto>> GetById(int id)
        {
            var enrollment = await _uniteOfWork._EnrollmentRepository.GetWithStudentAndCourses(id);
            if (enrollment == null)
                return CustomResult<EnrollmentDto>.Failure(CustomError.NotFoundError("Enrollment Not Found"));
            var enrollmentdto = _mapper.Map<EnrollmentDto>(enrollment);
            return CustomResult<EnrollmentDto>.Success(enrollmentdto);
        }

        public async Task<CustomResult> Update(int id, EnrollmentUpdateDto UpdateDto)
        {
            var enrollmentexist = await _uniteOfWork._EnrollmentRepository.Get(e => e.Id == id); 
            if (enrollmentexist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Enrollment You try To Update Not Found"));
            var studentExist = await _uniteOfWork._StudentRepository.Get(s => s.Id == UpdateDto.StudentID); ;
            if (studentExist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Student You try To Add Not Found"));
            var courseExist = await _uniteOfWork._CourseRepository.Get(c => c.Id == UpdateDto.CourseID);
            if (studentExist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Course You try To sign into Not Found"));
            enrollmentexist = UpdateDto.Adapt(enrollmentexist);
            await _uniteOfWork._EnrollmentRepository.Update(enrollmentexist);
            var complete = await _uniteOfWork.Save();
            if (complete == 0)
                return CustomResult.Failure(CustomError.NotFoundError("Faild in Update Enrollment"));
            return CustomResult.Success();
        }
    }
}
