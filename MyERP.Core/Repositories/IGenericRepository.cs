﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Where(Expression<Func<T,bool>> expression);
        Task AddAsync(T entity);
        int Count();
        void Update(T entity);
        void ChangeStatus(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    }
}
