using Microsoft.EntityFrameworkCore;
using MyERP.Core.Models;
using MyERP.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); // T is table in the db.
        }

        public async Task AddAsync(T entity)
        {
            // This is not what needs to be done, but the
            // correct way is currently bug in .NET8 version
            entity.Status = true; 

            await _dbSet.AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public void ChangeStatus(T entity)
        {
            _dbSet.Update(entity);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.Where(x => x.Status).AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id); // I'll added NotFound filter.
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where<T>(expression);
        }
    }
}
