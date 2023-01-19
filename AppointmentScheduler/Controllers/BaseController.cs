using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentScheduler.Controllers
{
    public class BaseController<T> : Controller where T : BaseController<T>
    {

        private IMapper mapper;
        private  ILogger<T> logger;
        protected string Alert { get; set; }

        protected ILogger<T> _Logger => logger?? (logger = HttpContext.RequestServices.GetRequiredService<ILogger<T>>());
        protected IMapper _mapper => mapper??( mapper= HttpContext.RequestServices.GetRequiredService<IMapper>());
        private string profileIdstring() 
        {
            if(HttpContext is not null && User is not null)
            {
                return User.FindFirst("profileId").Value;
            }
            else
            {
                return "0";
            }
        } 
        public int profileId
        {
            get { return Convert.ToInt32(profileIdstring()); }

        }
    }
}
