using AppointmentScheduler.Services;
using Common;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Authentication;
using Models.Registration;
using System.Security.Claims;

namespace AppointmentScheduler.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RepositoryService _repositoryService;
            
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RepositoryService repositoryService)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _repositoryService=repositoryService;
        }

        public IActionResult Login()
        {
            if (User is not null && User.Identity.IsAuthenticated) return RedirectToAction("Index", "Appointment");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationDTO authenticationDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(authenticationDTO.Username);
                if (user is null) return NotFound(new AuthenticationResponseDTO { IsSuccessful=false, Error ="User not found" });
                var profileId = (await _repositoryService.userProfileRepository.GetByCriteriaAsync(x => x.Username == user.UserName, false)).FirstOrDefault();
                if (profileId != null)
                {
                     await _userManager.AddClaimAsync(user, new Claim("profileId", $"{profileId.Id}", "int"));
                }
                var result = await _signInManager.PasswordSignInAsync(user, authenticationDTO.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Appointment");
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
                    var newProfile= await _repositoryService.userProfileRepository.CreateAsync(new UserProfile
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = user.UserName
                    });
                    if (newProfile is not null) await _repositoryService.Save();
                    var roleResult = user is null ? throw new ArgumentNullException("user", "user is null in registration") : await _userManager.AddToRoleAsync(user, SD.role_Customer);
                    if (!roleResult.Succeeded) throw new NotImplementedException("failed to add user to role");
                    return RedirectToAction(nameof(Login));

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


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }



    }
}
