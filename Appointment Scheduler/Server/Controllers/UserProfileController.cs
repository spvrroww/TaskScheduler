using Business.Helper;
using Business.Repository;
using Business.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Appointment_Scheduler.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserProfileController : BaseController<UserProfileController>
    {
       private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {        
           _userProfileRepository = userProfileRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var profile = await _userProfileRepository.GetByCriteriaAsync((x=> x.Id == id), false);
                return Ok(_mapper.Map<UserProfileDTO>(profile.FirstOrDefault()));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorModel { ErrorCode=StatusCodes.Status500InternalServerError, ErrorDesc="Operation Failed" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserProfileDTO userProfileDTO)
        {
            if (ModelState.IsValid)
            {
                var profile = await _userProfileRepository.CreateAsync(userProfileDTO.MapToUserProfile(_mapper));
                return Ok(profile.MapToUserProfileDTO(_mapper));
            }
            else
            {
                return BadRequest(new ErrorModel { ErrorCode=StatusCodes.Status400BadRequest, ErrorDesc = "Invalid Profile Details" });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Update(int id, UserProfileDTO userProfileDTO)
        {
            try
            {
                var profile = await _userProfileRepository.UpdateAsync(userProfileDTO.MapToUserProfile(_mapper));
                return Ok(profile.MapToUserProfileDTO(_mapper));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ErrorModel { ErrorCode=StatusCodes.Status500InternalServerError, ErrorDesc="Operation Failed" });
            }
        }


    }
}
