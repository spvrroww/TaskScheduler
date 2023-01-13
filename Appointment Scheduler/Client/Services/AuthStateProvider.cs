using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Appointment_Scheduler.Client.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<AuthStateProvider> _logger;
        private readonly HttpClient _httpClient;
        public AuthStateProvider(ILocalStorageService localStorage, ILogger<AuthStateProvider> logger, HttpClient httpClient)
        {
            _localStorage=localStorage;
            _logger=logger;
            _httpClient=httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await  _localStorage.GetItemAsync<string>("token");
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                if (handler.CanReadToken(token))
                {
                    var jwt = handler.ReadJwtToken(token);
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var identity = new ClaimsIdentity(jwt.Claims, "jwt");
                    return new AuthenticationState(new ClaimsPrincipal(identity));

                }
                else
                {
                    return new AuthenticationState(new ClaimsPrincipal());
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new AuthenticationState(new ClaimsPrincipal());
            }
        }

        public void  NotifyLogin()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void NotifyLogOut()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }
    }
}
