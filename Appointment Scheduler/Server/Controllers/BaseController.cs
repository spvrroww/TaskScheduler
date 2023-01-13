using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Scheduler.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        private IMapper mapper;
        private ILogger logger;
        

        protected IMapper _mapper => mapper?? (mapper=HttpContext.RequestServices.GetRequiredService<IMapper>());
        protected ILogger _logger => logger?? (logger=HttpContext.RequestServices.GetRequiredService<ILogger<T>>());
    }
}
