using AppointmentScheduler.Services;
using AppointmentScheduler.Services.ISevices;
using Business.Helper;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace AppointmentScheduler.Controllers
{
    public class AppointmentController : BaseController<AppointmentController>
    {
        private readonly RepositoryService _repositoryService;
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(RepositoryService repositoryService, IAppointmentService appointmentService)
        {
            _repositoryService = repositoryService;
            _appointmentService = appointmentService;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.alert = TempData["alert"];
            ViewBag.profileId = profileId;
            TempData["alert"] = null;
            ViewBag.tasks = await _appointmentService.GetAppointmentsFrequency(profileId, "month");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(AppointmentDTO appointmentDTO)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    var newAppointment =await  _repositoryService.appointmentRepository.CreateAsync(appointmentDTO.MapToAppointment(_mapper));

                    if(newAppointment != null)
                    {
                        TempData["alert"] = "Task Created";
                        await _repositoryService.Save();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["alert"] = "Failed to create task";
                        return RedirectToAction(nameof(Index));
                    }
                }

                TempData["alert"] = "Incorrect appointment details";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                TempData["alert"] = "Failed to create appointment";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEvent(AppointmentDTO appointmentDTO)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    appointmentDTO.ProfileId = profileId;
                    var existingAppointment = await _repositoryService.appointmentRepository.GetByCriteriaAsync(x=> x.Id == appointmentDTO.Id && x.profileId == profileId, true);
                    
                    if (existingAppointment != null)
                    {
                        var updated = await _repositoryService.appointmentRepository.UpdateAsync(appointmentDTO.MapToAppointment(_mapper));
                        if(updated is null)
                        {
                            TempData["alert"] = "Failed to update task";
                        }
                        else
                        {
                            TempData["alert"] = "Updated successfully";
                            await _repositoryService.Save();
                        }
                        
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["alert"] = "Failed to update task";
                        return RedirectToAction(nameof(Index));
                    }
                }

                TempData["alert"] = "Incorrect  details";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                TempData["alert"] = "Failed to update task";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                var appointment = (await _repositoryService.appointmentRepository.GetByCriteriaAsync(x => x.Id == id, true)).FirstOrDefault();
                if(appointment is not null)
                {
                    _repositoryService.appointmentRepository.Delete(appointment);
                    TempData["alert"] = await _repositoryService.Save()? "Deleted Successfully" :"Failed to delete Task" ;
                    return RedirectToAction(nameof(Index));

                }
                throw new ArgumentNullException("appointment", "appointment is null in Delete event");
               

            }
            catch(Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                TempData["alert"] = "Failed to delete task";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]  
        public async Task<JsonResult> GetAllEvents(DateTime startDate, DateTime endDate)
        {
            try
            {
                var tasks = (await  _repositoryService.appointmentRepository.GetByCriteriaAsync(x => x.profileId == profileId&& x.Start.Date >= startDate.Date && x.End.Date <= endDate.Date, false))?.Select(x=>x.MapToAppointmentDTO(_mapper))?.ToList();
              
                return Json(tasks);
            }
            catch(Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return Json(null);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetStats()
        {
            try
               {
                var stats = await _appointmentService.GetAppointmentsStats(profileId);
                return Json(stats);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return Json(null);
            }
        }


    }
}
