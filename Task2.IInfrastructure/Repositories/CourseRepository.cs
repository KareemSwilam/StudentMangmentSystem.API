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
    public class CourseRepository: Repository<Course> ,ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context) { }
    }
}
