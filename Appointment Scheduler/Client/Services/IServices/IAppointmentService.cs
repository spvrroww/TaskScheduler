using Models;

namespace Appointment_Scheduler.Client.Services.IServices
{
    public interface IAppointmentService
    {
        public Task<AppointmentDTO> CreateAsync(AppointmentDTO appointmentDTO);
        public Task<AppointmentDTO> UpdateAsync(int id,AppointmentDTO appointmentDTO);
        public Task<bool> DeleteAsync(int id);
        public Task<AppointmentDTO> GetAsync(int id);
        public Task<List<AppointmentDTO>> GetAllAsync(int profileId);
    }
}
