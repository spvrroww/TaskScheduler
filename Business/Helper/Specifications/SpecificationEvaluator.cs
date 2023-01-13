using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> BuidQuery<TEntity>(IQueryable<TEntity> inputQueryable, Specification<TEntity> specification)
        {
            try
            {
                var queryable = inputQueryable.Where(specification.Criteria);

                if(specification.OrderBy is not null)
                {
                    queryable = queryable.OrderBy(specification.OrderBy);
                }

                if(specification.OrderByDescending is not null)
                {
                    queryable = queryable.OrderByDescending(specification.OrderByDescending);
                }

                if(specification.Skip is not null)
                {
                    queryable = queryable.Skip(specification.Skip.Value);
                }

                if (specification.Skip is not null)
                {
                    queryable = queryable.Skip(specification.Take.Value);
                }

                return queryable;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
