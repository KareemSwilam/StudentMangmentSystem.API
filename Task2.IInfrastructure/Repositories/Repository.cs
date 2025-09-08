using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task2.Domain.Interfaces;
using Task2.Infrastructure.Persistence;

namespace Task2.Infrastructure.Repositories
{
    public class Repository<T>:IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T item)
        {
            await _dbSet.AddAsync(item);
        }

        public Task Delete(T item)
        {
            _dbSet.Remove(item);
            return Task.CompletedTask;
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter, bool traking = true)
        {
            IQueryable<T> query = _dbSet;
            if (!traking)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);

            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public Task Update(T item)
        {
            _dbSet.Update(item);
            return Task.CompletedTask;
        }
    }
}
