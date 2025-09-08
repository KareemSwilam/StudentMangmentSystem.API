using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Domain.Models;

namespace Task2.Domain.Interfaces
{
    public interface IEnrollmentRepository:IRepository<Enrollment>
    {
        public Task<List<Enrollment>> GetAllWithStudentAndCourses();
        public Task<Enrollment> GetWithStudentAndCourses(int id);
    }
}
