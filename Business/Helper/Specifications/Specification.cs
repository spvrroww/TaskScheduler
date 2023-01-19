using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper.Specifications
{
    public abstract class Specification<TEntity>
    {
        public Specification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<TEntity,bool>> Criteria { get; private set; }
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
      
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

        public void AddOrderBy(Expression<Func<TEntity, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        public void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }

        public void AddSkip(int skip)
        {
            Skip = skip;
        }

        public void AddTake(int take)
        {
            Take = take;
        }
      





    }
}
