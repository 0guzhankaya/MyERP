﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Core.Services
{
    public interface IGenericService<T> where T: class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        int Count();
        void Update(T entity);
        void ChangeStatus(T entity);
    }
}
