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
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext db): base(db)
        {
        }

        //public async Task<Appointment> CreateAppointment(Appointment appointment) => await CreateAsync(appointment);

        //public async Task<Appointment> GetAppointment(int id, bool trackChanges) => (await GetByCriteriaAsync(x => x.Id == id, trackChanges)).FirstOrDefault();

        //public async Task<IEnumerable<Appointment>> GetAppointments(int profileId, bool trackChanges) => await GetByCriteriaAsync(x => x.profileId == profileId, trackChanges);

        //public async Task<bool> DeleteAppointment(int id)
        //{
        //    var appointment = await GetAppointment(id, true);
        //    if(appointment is not null)
        //    {
        //         Delete(appointment);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


    }
}
