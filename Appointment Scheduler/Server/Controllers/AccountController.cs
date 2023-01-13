using Appointment_Scheduler.Server.Helper;
using Business.Repository;
using Business.Repository.IRepository;
using Business.Helper;
using Common;
using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Authentication;
using Models.Registration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Appointment_Scheduler.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : BaseController<AccountController>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserProfileRepository _userProfileRepository;
  
        private readonly JwtHelper _jwtHelper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            RoleManager<IdentityRole> roleManager, IUserProfileRepository userProfileRepository, 
           IOptions<JwtHelper> jwtHelper)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _roleManager=roleManager;
            _userProfileRepository=userProfileRepository;
           
            _jwtHelper=jwtHelper.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationDTO authenticationDTO)
        {
            //throw new Exception("Test Exception thrown", new UnauthorizedAccessException());
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = await _userManager.FindByEmailAsync(authenticationDTO.Username);
                    if (existingUser is null) return BadRequest(new AuthenticationResponseDTO { IsSuccessful=false, Error="User not found" });
                    var result = await _signInManager.PasswordSignInAsync(existingUser, authenticationDTO.Password, false, false);
                    if (result.Succeeded)
                    {
                        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                            issuer: _jwtHelper.ValidIssuer,
                            audience: _jwtHelper.ValidAudience,
                            claims: GetClaims(existingUser),
                            expires: DateTime.Now.AddMonths(2),
                            signingCredentials: GetSigningCredentials()
                            );

                        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                        var token = handler.WriteToken(jwtSecurityToken);

                        return Ok(new AuthenticationResponseDTO
                        {
                            IsSuccessful = true,
                            Token = token
                        });
                        
                           
                       
                    }
                    else
                    {
                         return BadRequest(new AuthenticationResponseDTO { IsSuccessful=false, Error="Invalid Log in details, try again" });

                    }
                }
                else
                {
                    return BadRequest(new AuthenticationResponseDTO { IsSuccessful=false, Error="Invalid Log in details, try again" });
                }
               
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorModel
                {
                    ErrorCode= StatusCodes.Status500InternalServerError,
                    ErrorDesc= "Login Failed"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationDTO)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var existingUser = await _userManager.FindByEmailAsync(registrationDTO.Email);
                    if (existingUser is not null) return BadRequest(new RegistrationResponseDTO
                    {
                        IsSuccessful = false,
                        Error ="Cannot use email, try a different one"
                    });

                    AppUser appUser = new AppUser
                    {
                        FirstName = registrationDTO.FirstName,
                        LastName = registrationDTO.LastName,
                        UserName = registrationDTO.Email,
                        Email = registrationDTO.Email
                    };

                    var result = await _userManager.CreateAsync(appUser, registrationDTO.Password);
                    if (result.Succeeded)
                    {
                        var newUser = await _userManager.FindByEmailAsync(registrationDTO.Email);
                        var roleResult = await _userManager.AddToRoleAsync(newUser, SD.role_Customer);
                        if (roleResult.Succeeded)
                        {
                            var profile = await _userProfileRepository.CreateAsync(new UserProfile { FirstName = newUser.FirstName, LastName = newUser.LastName, Username = newUser.UserName });
                            await _userProfileRepository.Save();
                            if (profile is not null)
                            {
                                return Ok(new RegistrationResponseDTO { IsSuccessful = true, Profile = profile.MapToUserProfileDTO(_mapper) });
                            }
                            else
                            {
                                throw new Exception("Failed to Create User Profile");
                            }
                        }
                        else
                        {
                            throw new Exception("Failed to Add User to role");
                        }
                    }
                    else
                    {
                        throw new Exception("Failed to Create User ");
                    }


                }
                else
                {
                    return BadRequest(new RegistrationResponseDTO { IsSuccessful=false, Error ="Invalid Request" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorModel { ErrorCode = StatusCodes.Status500InternalServerError , ErrorDesc ="Registration failed" });
            }
         
        }

        private List<Claim> GetClaims(AppUser appUser)
        {
            List<Claim> claims = new();
            claims.Add(new Claim(ClaimTypes.Email, appUser.Email));
            claims.Add(new Claim(ClaimTypes.Name, appUser.Id));
            claims.Add(new Claim("FirstName", appUser.FirstName));
            claims.Add(new Claim("LastName", appUser.LastName));
            
            var roles =  _userManager.GetRolesAsync(appUser).GetAwaiter().GetResult();
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;

        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtHelper.SecretKey));
            return new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        }




    }
}
