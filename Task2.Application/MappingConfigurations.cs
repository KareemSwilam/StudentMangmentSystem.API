using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.CourseDtos;
using Task2.Application.Dtos.DepartmentDtos;
using Task2.Application.Dtos.EnrollmentDtos;
using Task2.Application.Dtos.StudentDtos;
using Task2.Domain.Models;

namespace Task2.Application
{
    public class MappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Student, StudentDto>();
            config.NewConfig<StudentDto, Student>();
            config.NewConfig<StudentCreateDto, Student>();
            config.NewConfig<StudentUpdateDto, Student>();
            config.NewConfig<Department,DepartmentDto>(); 
            config.NewConfig<DepartmentDto, Department>();
            config.NewConfig<DepartmentCreateDto, Department>();
            config.NewConfig<DepartmentUpdateDto, Department>();
            config.NewConfig<Course, CourseDto>();
            config.NewConfig<CourseDto, Course>();
            config.NewConfig<CourseCreateDto, Course>();
            config.NewConfig<CourseUpdateDto, Course>();
            config.NewConfig<Enrollment, EnrollmentDto>();
            config.NewConfig<EnrollmentDto, Enrollment>();
            config.NewConfig<EnrollmentCreateDto, Enrollment>();
            config.NewConfig<EnrollmentUpdateDto, Enrollment>();
        }
    }
}
