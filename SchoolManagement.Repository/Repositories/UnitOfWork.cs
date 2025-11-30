using SchoolManagement.Data.Contexts;
using SchoolManagement.Data.Entities;
using SchoolManagement.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolManagementContext _context;
        private Hashtable _repositories;

        public UnitOfWork(SchoolManagementContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new GenericRepository<T>(_context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>)_repositories[type];
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task<int> CountAsync() => throw new NotImplementedException(); 
    }
}
