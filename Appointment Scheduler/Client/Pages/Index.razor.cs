using Appointment_Scheduler.Client.Services.IServices;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models.Authentication;

namespace Appointment_Scheduler.Client.Pages
{
    public partial class Index
    {
        private AuthenticationDTO authenticationDTO = new AuthenticationDTO();
        [Inject]
        NavigationManager _navigationManager { get; set; }
        [Inject]
        IAccountService _accountService { get; set; }
        [Inject]
        IJSRuntime jsRuntime { get; set; }

        private string LoginError { get; set; }





        private async Task Login()
        {
            var authResponse = await _accountService.Login(authenticationDTO);
            if (authResponse is not null && authResponse.IsSuccessful)
            {
                _navigationManager.NavigateTo("/dashboard");
            }
            else
            {
                LoginError = authResponse.Error;
                jsRuntime.InvokeVoidAsync("ShowError", "loginError");
            }
        }
    }
}
