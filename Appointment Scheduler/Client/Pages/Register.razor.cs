using Appointment_Scheduler.Client.Services.IServices;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models.Registration;

namespace Appointment_Scheduler.Client.Pages
{
    public partial class Register
    {

        [Inject]
        public IAccountService _accountService { get; set; }

        [Inject]
        public NavigationManager  _navigationManager { get; set; }

        [Inject]
        public IJSRuntime jsRuntime { get; set; }


        private RegistrationDTO registrationDTO = new RegistrationDTO();
        private string RegError { get; set; }

        private async Task HandleRegister()
        {
            var registrationResponse = await _accountService.Register(registrationDTO);
            if(registrationResponse != null && registrationResponse.IsSuccessful)
            {

                _navigationManager.NavigateTo("/");
            }
            else
            {
                RegError = "Registration failed";
               await jsRuntime.InvokeVoidAsync("ShowError", "regerror");
            }
        }
    }
}
