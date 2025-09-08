using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Models;
using Task2.Core.Models.Dtos;

namespace Task2.Core.IRepository
{
    public interface IDepartmentRepository: IRepository<Department>
    {
        Task<DepartmentDto> GetCourses(int id);
    }
}
