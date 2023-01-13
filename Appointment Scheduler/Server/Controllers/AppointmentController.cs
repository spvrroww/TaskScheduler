using Business.Helper;
using Business.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Appointment_Scheduler.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentController : BaseController<AppointmentController>
    {
        private readonly AppointmentRepository _appointmentRepository;

        public AppointmentController(AppointmentRepository appointmentRepository)
        {
            _appointmentRepository=appointmentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentDTO appointmentDTO)
        {
            if (ModelState.IsValid)
            {
                var appointment = await _appointmentRepository.CreateAsync(appointmentDTO.MapToAppointment(_mapper));

                return Ok(appointment.MapToAppointmentDTO(_mapper));
            }
            else
            {
                return BadRequest(new ErrorModel { ErrorCode = StatusCodes.Status400BadRequest, ErrorDesc = "Invalid Request params" });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {           
                var appointment = await _appointmentRepository.GetByCriteriaAsync(x=>x.Id == id, false);
                return Ok(appointment.FirstOrDefault().MapToAppointmentDTO(_mapper));   
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentDTO appointmentDTO)
        {
            if (ModelState.IsValid)
            {
                var appointment = await _appointmentRepository.UpdateAsync(appointmentDTO.MapToAppointment(_mapper));
                return Ok(appointment.MapToAppointmentDTO(_mapper));
            }
            else
            {
                return BadRequest(new ErrorModel { ErrorCode = StatusCodes.Status400BadRequest, ErrorDesc = "Invalid Request params" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = (await _appointmentRepository.GetByCriteriaAsync(x => x.Id == id, true)).FirstOrDefault();
            if(entity is not null)
            {
                 _appointmentRepository.Delete(entity);
                
            }
            return Ok(_appointmentRepository.Save());

        }

        [HttpGet("{profileId:int}")]
        public async Task<IActionResult> GetAllByUserByDateOrder(int profileId)
        {    
                var appointment = (await _appointmentRepository.GetByCriteriaAsync(x=>x.profileId == profileId, false)).FirstOrDefault();
                return Ok(appointment.MapToAppointmentDTO(_mapper));
        }



    }
}
