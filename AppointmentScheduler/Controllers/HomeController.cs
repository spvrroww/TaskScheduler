using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
