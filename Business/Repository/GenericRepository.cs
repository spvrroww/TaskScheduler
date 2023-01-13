using Business.Repository.IRepository;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{ 
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        
        private readonly ApplicationDbContext _db;
        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<T> CreateAsync(T entity) => (await _db.Set<T>().AddAsync(entity)).Entity;


        public void Delete(T entity) => _db.Set<T>().Remove(entity);

        public async Task<IQueryable<T>> GetAllAsync(bool trackChanges) => trackChanges ? _db.Set<T>() : _db.Set<T>().AsNoTracking();
      

        public async Task<IQueryable<T>> GetByCriteriaAsync(Expression<Func<T, bool>> criteria, bool trackChanges) => trackChanges? _db.Set<T>().Where(criteria) : _db.Set<T>().Where(criteria).AsNoTracking();

        public async Task<T> UpdateAsync(T entity) => (_db.Set<T>().Update(entity)).Entity;

        public async Task<bool> Save() => await _db.SaveChangesAsync() > 0;
       
    }
}
