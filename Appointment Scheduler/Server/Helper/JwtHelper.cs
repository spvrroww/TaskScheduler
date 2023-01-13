using System;

namespace Appointment_Scheduler.Server.Helper
{
    public class JwtHelper
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string SecretKey { get; set; }
    }
}
