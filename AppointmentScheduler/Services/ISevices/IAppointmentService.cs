namespace AppointmentScheduler.Services.ISevices
{
    public interface IAppointmentService
    {
        public Task<Dictionary<string, int>> GetAppointmentsStats(int profileId);
        public Task<List<KeyValuePair<string, int>>> GetAppointmentsFrequency(int profileId, string type);
    }
}
