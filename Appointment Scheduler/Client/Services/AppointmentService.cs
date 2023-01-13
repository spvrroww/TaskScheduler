using Appointment_Scheduler.Client.Services.IServices;
using Models;
using Newtonsoft.Json;

namespace Appointment_Scheduler.Client.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HttpClient _client;
        public AppointmentService(HttpClient client)
        {
            _client=client;
        }

        public async Task<AppointmentDTO> CreateAsync(AppointmentDTO appointmentDTO)
        {
            var response = await RequestHandler.HandlePostRequest(_client, "api/appointment/create", appointmentDTO);
          
            if (response.Item1)
            {
                var appointment = JsonConvert.DeserializeObject<AppointmentDTO>(response.Item2);
                return appointment;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await RequestHandler.HandleDeleteRequest(_client, $"api/appointment/Delete/{id}");

            if (response.Item1)
            {
                var appointment = JsonConvert.DeserializeObject<AppointmentDTO>(response.Item2);
                if(appointment == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<List<AppointmentDTO>> GetAllAsync(int profileId)
        {
            var response = await RequestHandler.HandleGetRequest(_client, $"api/appointment/GetAllByUserByDateOrder/{profileId}");

            if (response.Item1)
            {
                var appointment = JsonConvert.DeserializeObject<List<AppointmentDTO>>(response.Item2);
                return appointment;
            }
            else
            {
                return null;
            }
        }

        public async Task<AppointmentDTO> GetAsync(int id)
        {
            var response = await RequestHandler.HandleGetRequest(_client, $"api/appointment/GetAllByUserByDateOrder/{id}");

            if (response.Item1)
            {
                var appointment = JsonConvert.DeserializeObject<AppointmentDTO>(response.Item2);
                return appointment;
            }
            else
            {
                return null;
            }
        }

        public async Task<AppointmentDTO> UpdateAsync(int id, AppointmentDTO appointmentDTO)
        {
            var response = await RequestHandler.HandlePostRequest(_client, $"api/appointment/create/{id}", appointmentDTO);

            if (response.Item1)
            {
                var appointment = JsonConvert.DeserializeObject<AppointmentDTO>(response.Item2);
                return appointment;
            }
            else
            {
                return null;
            }
        }
    }
}
