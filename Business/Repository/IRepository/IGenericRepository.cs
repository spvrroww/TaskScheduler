
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IGenericRepository<T>
    {
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task<IQueryable<T>> GetByCriteriaAsync(Expression<Func<T, bool>> criteria, bool trackChanges);
        public Task<IQueryable<T>> GetAllAsync(bool trackChanges);
        public void Delete(T entity);
      
    }
}
