using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Expression<Func<T, bool>> filter, bool traking = true);
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);
        Task Add(T item);
        Task Delete(T item);
        Task Update(T item);
    }
}
