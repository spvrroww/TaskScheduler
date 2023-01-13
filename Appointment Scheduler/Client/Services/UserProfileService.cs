using Appointment_Scheduler.Client.Services.IServices;
using Models;
using Newtonsoft.Json;

namespace Appointment_Scheduler.Client.Services
{
    public class UserProfileService : IUserProfileService
    {

        private readonly HttpClient _client;

        public UserProfileService(HttpClient client)
        {
            _client=client;
        }


        public async Task<UserProfileDTO> GetAsync(int id)
        {
            var response = await RequestHandler.HandleGetRequest(_client, $"api/userprofile/get/{id}");
            if (response.Item1)
            {
                var profile = JsonConvert.DeserializeObject<UserProfileDTO>(response.Item2);
                return profile;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserProfileDTO> UpdateAsync(int id, UserProfileDTO userProfileDTO)
        {
            var response = await RequestHandler.HandlePutRequest(_client, $"api/userprofile/update/{id}", userProfileDTO);
            if (response.Item1)
            {
                var profile = JsonConvert.DeserializeObject<UserProfileDTO>(response.Item2);
                return profile;
            }
            else
            {
                return null;
            }
        }
    }
}
