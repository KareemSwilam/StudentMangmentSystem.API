using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.IRepository;
using Task2.Core.Models;
using Task2.Core.Models.Dtos;

namespace Task2.EF.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly IMapper _mapper;
        public DepartmentRepository(ApplicationContext context , IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<DepartmentDto> GetCourses(int id)
        {
            IQueryable<Department> department =  base._dbSet.Where(d => d.Id == id).Include(d => d.Courses);
            var departments =  await department.FirstAsync();
            var departmentdto = _mapper.Map<DepartmentDto>(departments);
            return departmentdto;
           
            
        }
    }
}
