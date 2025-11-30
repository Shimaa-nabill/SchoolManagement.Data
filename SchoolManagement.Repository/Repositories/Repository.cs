using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data.Contexts;
using SchoolManagement.Data.Entities;
using SchoolManagement.Repository.Interfaces;
using SchoolManagement.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly SchoolManagementContext _context;
        public GenericRepository(SchoolManagementContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            var query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
            return await query.ToListAsync();
        }

        public async Task<int> CountWithSpecAsync(ISpecification<T> spec)
        {
            var query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
            return await query.CountAsync();
        }

        public async Task<IReadOnlyList<TResult>> GetDistinctAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await _context.Set<T>().Select(selector).Distinct().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithIncludeAsync<TProperty>(Expression<Func<T, TProperty>> include)
        {
            return await _context.Set<T>().Include(include).ToListAsync();
        }
    }
}
