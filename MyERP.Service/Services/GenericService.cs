using MyERP.Core.Models;
using MyERP.Core.Repositories;
using MyERP.Core.Services;
using MyERP.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Service.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWorks _unitOfWorks;

        public GenericService(IGenericRepository<T> repository, IUnitOfWorks unitOfWorks)
        {
            _repository = repository;
            _unitOfWorks = unitOfWorks;
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;

            await _repository.AddAsync(entity);
            await _unitOfWorks.CommitAsync();
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public void ChangeStatus(T entity)
        {
            entity.UpdatedDate = DateTime.Now; // Kim ne zaman sildi kardeşim?
            _repository.ChangeStatus(entity);
            _unitOfWorks.Commit();
        }

        public int Count()
        {
            return _repository.Count();
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
            _unitOfWorks.Commit();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
