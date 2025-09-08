using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Models;

namespace Task2.Core.IRepository
{
    public interface ICourseRepository:IRepository<Course>
    {
        Task<List<Student>> GetStudents(int id);
    }
}
