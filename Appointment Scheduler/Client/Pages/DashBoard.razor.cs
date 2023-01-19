using Models;

namespace Appointment_Scheduler.Client.Pages
{
    public partial class DashBoard
    {
        public AppointmentDTO Appointment { get; set; } = new AppointmentDTO { Start= DateTime.Now, End =DateTime.Now };
        
        //private void HandleDateChange(DateTime value)
        //{
        //    Appointment.Start = value;
        //    //Appointment.End = Appointment.Start;
        //}
    }
}
