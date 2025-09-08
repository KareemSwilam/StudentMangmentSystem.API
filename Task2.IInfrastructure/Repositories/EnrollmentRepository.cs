using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Domain.Interfaces;
using Task2.Domain.Models;
using Task2.Infrastructure.Persistence;

namespace Task2.Infrastructure.Repositories
{
    public class EnrollmentRepository: Repository<Enrollment>,IEnrollmentRepository
    {
        
        public EnrollmentRepository(AppDbContext context) : base(context) { }

        public async Task<List<Enrollment>> GetAllWithStudentAndCourses()
        {
            IQueryable<Enrollment> enrollments = _context.enrollments.Include(e => e.Course).Include(e => e.Student);
            return await enrollments.ToListAsync();
        }

        public async Task<Enrollment> GetWithStudentAndCourses(int id)
        {
            IQueryable<Enrollment> enrollments = _context.enrollments.Where(e => e.Id == id).Include(e => e.Course).Include(e => e.Student);
            return await enrollments.FirstOrDefaultAsync();
        }
    }
}
