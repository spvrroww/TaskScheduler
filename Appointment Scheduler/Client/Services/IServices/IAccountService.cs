using Models.Authentication;
using Models.Registration;

namespace Appointment_Scheduler.Client.Services.IServices
{
    public interface IAccountService
    {
        public Task<AuthenticationResponseDTO> Login(AuthenticationDTO authenticationDTO);
        public Task<RegistrationResponseDTO> Register(RegistrationDTO registrationDTO);
        public void SignOut();

    }
}
