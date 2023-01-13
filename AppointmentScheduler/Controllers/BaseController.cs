using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.Controllers
{
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        private readonly ILogger<T> logger;

        protected ILogger<T> _Logger => logger?? HttpContext.RequestServices.GetRequiredService<ILogger<T>>();

        
    }
}
