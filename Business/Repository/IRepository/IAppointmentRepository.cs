using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IAppointmentRepository:IGenericRepository<Appointment>
    {
        //public Task<Appointment> CreateAppointment(Appointment appointment);
        //public Task<IEnumerable<Appointment>> GetAppointments(int profileId, bool trackChanges);
        //public Task<Appointment> GetAppointment(int id, bool trackChanges);
        //public Task<bool> DeleteAppointment(int id);
    }
}
