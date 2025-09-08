using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.IRepository
{
    public interface IUniteOfWork : IDisposable
    {
        IDepartmentRepository _DepartmentRepository {  get; }
         IStudentRepository _StudentRepository { get; }
         ICourseRepository _CourseRepository { get; }
         IEnrollmentRepository _EnrollmentRepository { get; }
         Task<int> Save();
       
    }
}
