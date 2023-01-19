using AppointmentScheduler.Services.ISevices;
using Common;
using DataAccess.Data;

namespace AppointmentScheduler.Services
{   //Tasks analytical services
    public class AppointmentService : IAppointmentService
    {
        private readonly RepositoryService _repositoryService;

        public AppointmentService(RepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public async Task<List<KeyValuePair<string, int>>> GetAppointmentsFrequency(int profileId, string type)
        {
            try
            {
                IEnumerable<Appointment> appointments;
                switch (type)
                {
                    case "month":
                        appointments = await _repositoryService.appointmentRepository.GetByCriteriaAsync(X => X.profileId == profileId && X.Start.Month == DateTime.Now.Month, false);
                        break;
                    case "year":
                        appointments = await _repositoryService.appointmentRepository.GetByCriteriaAsync(X => X.profileId == profileId && X.Start.Year == DateTime.Now.Year, false);
                        break;
                    default:
                        appointments = Enumerable.Empty<Appointment>();
                        break;
                }

                 
                Dictionary<string, int> taskCount = new Dictionary<string, int>();
                foreach (var task in appointments)
                {
                    if (taskCount.ContainsKey(task.Title))
                    {
                        taskCount[task.Title]++;
                    }
                    else
                    {
                        taskCount.Add(task.Title, 1);
                    }
                }

                return taskCount.OrderByDescending(x=> x.Value).Take(10).ToList();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Dictionary<string, int>> GetAppointmentsStats(int profileId)
        {
            try
            {
                Dictionary<string, int> stats = new Dictionary<string, int>();  
                foreach(var category in SD.categories)
                {
                    int count = await _repositoryService.appointmentRepository.GetCountByCriteria(x=>x.profileId == profileId && x.Category == category);
                    stats.Add(category, count);
                }

                return stats;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
