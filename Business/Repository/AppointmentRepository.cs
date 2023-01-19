using AutoMapper;
using Business.Helper.Specification.AppointmentSpecifications;
using Business.Helper.Specifications;
using Business.Repository.IRepository;
using DataAccess;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _db;
        public AppointmentRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

  

        public async Task<int> GetCountByCriteria(Expression<Func<Appointment, bool>> criteria)
        {     
            return _db.Appointments.Count(criteria);
        }

        
    }
}
