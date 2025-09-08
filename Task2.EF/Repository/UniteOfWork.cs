using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.IRepository;

namespace Task2.EF.Repository
{
    public class UniteOfWork : IUniteOfWork
    {
        
        private readonly ApplicationContext _context;
        public IDepartmentRepository _DepartmentRepository { get; private set; }
        public IStudentRepository _StudentRepository { get; private set; }
        public ICourseRepository _CourseRepository { get; private set; }
        public IEnrollmentRepository _EnrollmentRepository { get; private set; }
        public UniteOfWork(ApplicationContext context, IDepartmentRepository DepartmentRepository,
            IStudentRepository StudentRepository, ICourseRepository CourseRepository,
            IEnrollmentRepository EnrollmentRepository)
        {
            _context = context;
            _DepartmentRepository = DepartmentRepository;
            _StudentRepository = StudentRepository;
            _CourseRepository = CourseRepository;
            _EnrollmentRepository = EnrollmentRepository;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
