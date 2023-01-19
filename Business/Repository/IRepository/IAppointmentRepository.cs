using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IAppointmentRepository:IGenericRepository<Appointment>
    {
        public Task<int> GetCountByCriteria(Expression<Func<Appointment, bool>> criteria);

    }
}
