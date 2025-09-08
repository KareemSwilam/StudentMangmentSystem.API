using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Models;
using Task2.Core.Models.Dtos;

namespace Task2.Core
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Student, StudentCreateDto>();
            config.NewConfig<StudentCreateDto, Student>();
            config.NewConfig<Student, StudentUpdateDto>();
            config.NewConfig<StudentUpdateDto, Student>();
            config.NewConfig<Department, DepartmentCreateDto>();
            config.NewConfig<DepartmentCreateDto, Department>();
            config.NewConfig<Department, DepartmentUpdateDto>();
            config.NewConfig<DepartmentUpdateDto, Department>();
            config.NewConfig<Course, CourseCreateDto>();
            config.NewConfig<CourseCreateDto, Course>();
            config.NewConfig<Course, CourseUpdateDto>();
            config.NewConfig<CourseUpdateDto, Course>();
            config.NewConfig<Enrollment, EnrollmentCreateDto>();
            config.NewConfig<EnrollmentCreateDto, Enrollment>();
            config.NewConfig<Enrollment, EnrollmentUpdateDto>();
            config.NewConfig<EnrollmentUpdateDto, Enrollment>();
            config.NewConfig<Department, DepartmentDto>();
            config.NewConfig<DepartmentDto, Department>();
            config.NewConfig<CourseDto, Course>();
            config.NewConfig<Course, CourseDto>();
        }
    }
}
