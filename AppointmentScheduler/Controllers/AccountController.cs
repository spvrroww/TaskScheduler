using Common;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Authentication;
using Models.Registration;

namespace AppointmentScheduler.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
            
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager=userManager;
            _signInManager=signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationDTO authenticationDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(authenticationDTO.Username);
                if (user is null) return NotFound(new AuthenticationResponseDTO { IsSuccessful=false, Error ="User not found" });
                var result = await _signInManager.PasswordSignInAsync(user, authenticationDTO.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return View();
            }
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationDTO registrationDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    AppUser appUser = new AppUser
                    {
                        FirstName = registrationDTO.FirstName,
                        LastName = registrationDTO.LastName,
                        Email = registrationDTO.Email,
                        UserName = registrationDTO.Email

                    };

                    var result = await _userManager.CreateAsync(appUser, registrationDTO.Password);
                    if (!result.Succeeded) throw new NotImplementedException("Failed to Create new User");
                    var user = await _userManager.FindByEmailAsync(appUser.Email);
                    var roleResult = user is null ? throw new ArgumentNullException("user", "user is null in registration") : await _userManager.AddToRoleAsync(user, SD.role_Customer);
                    if (!roleResult.Succeeded) throw new NotImplementedException("failed to add user to role");
                    return View();

                }
                else
                {
                    return View();
                }
                
            }
            catch(Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return View();
            }
        }






    }
}
