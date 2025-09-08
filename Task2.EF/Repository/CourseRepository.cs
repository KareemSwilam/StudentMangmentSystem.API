using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.IRepository;
using Task2.Core.Models;

namespace Task2.EF.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<Student>> GetStudents(int id)
        {
            var course = _context.courses.Where( c=> c.Id == id);
            var students = course
                .Join(_context.enrollments, c => c.Id, e => e.CourseID, (c, e) => new { e.CourseID, e.StudentID })
                .Join(_context.students, e => e.StudentID, s => s.Id, (e, s) => s);
            return await students.ToListAsync();
        }
    }
}
