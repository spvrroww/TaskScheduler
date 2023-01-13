using Models;

namespace Appointment_Scheduler.Client.Services.IServices
{
    public interface IUserProfileService
    {
        public Task<UserProfileDTO> GetAsync(int id);
        public Task<UserProfileDTO> UpdateAsync(int id, UserProfileDTO userProfileDTO);
    }
}
