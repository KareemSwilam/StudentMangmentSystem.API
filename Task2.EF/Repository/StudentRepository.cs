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
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationContext context) : base(context) { }

        public async Task<List<Course>> GetCourses(int id)
        {
            var student = _context.students.Where(s => s.Id == id);
            var Courses = student
                .Join(_context.enrollments, s => s.Id, e => e.StudentID, (s, e) => new { e.CourseID })
                .Join(_context.courses, e => e.CourseID, c => c.Id, (e, c) => c);
            return await Courses.ToListAsync();
        }
    }
}
