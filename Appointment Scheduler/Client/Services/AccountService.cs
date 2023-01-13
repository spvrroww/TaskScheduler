using Appointment_Scheduler.Client.Services.IServices;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using Models.Authentication;
using Models.Registration;
using Newtonsoft.Json;
using System.Text;

namespace Appointment_Scheduler.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<AccountService> _logger;


        public AccountService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorageService, ILogger<AccountService> logger)
        {
            _client = client;
            _authStateProvider=authStateProvider;
            _localStorage = localStorageService;
            _logger=logger;
        }

        public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO authenticationDTO)
        {
         
               
                var response = await RequestHandler.HandlePostRequest(_client, "api/account/login", authenticationDTO);
                if (response.Item1)
                {
                    var authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(response.Item2);
                    if (authenticationResponse.IsSuccessful)
                    {
                        await _localStorage.SetItemAsync<string>("token", authenticationResponse.Token);
                        ((AuthStateProvider)_authStateProvider).NotifyLogin();
                        ((AuthStateProvider)_authStateProvider).NotifyLogin();


                        return new AuthenticationResponseDTO { IsSuccessful = true };
                    }
                    else
                    {
                        return new AuthenticationResponseDTO { IsSuccessful = false };
                    }
                }
                else
                {
                    return new AuthenticationResponseDTO { IsSuccessful = false };
                }
            
       
        }

        public async Task<RegistrationResponseDTO> Register(RegistrationDTO registrationDTO)
        {
            var response = await RequestHandler.HandlePostRequest(_client, "api/account/register", registrationDTO);
            if (response.Item1)
            {
                var registrationResponse = JsonConvert.DeserializeObject<RegistrationResponseDTO>(response.Item2);
                await _localStorage.SetItemAsync<UserProfileDTO>("userDetails", registrationResponse.Profile);
                return new RegistrationResponseDTO { IsSuccessful = registrationResponse.IsSuccessful };

            }
            else
            {
                return new RegistrationResponseDTO { IsSuccessful = false };
            }
         
     
        }

        public void SignOut()
        {
            ((AuthStateProvider)_authStateProvider).NotifyLogOut();
        }
    }
}
