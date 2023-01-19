using Business.Repository;
using Business.Repository.IRepository;
using DataAccess;

namespace AppointmentScheduler.Services
{ // A repository manager service for all repositories
    public class RepositoryService
    {
        
        private readonly IAppointmentRepository _repository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ApplicationDbContext _db;

        public RepositoryService(IAppointmentRepository repository, ApplicationDbContext db, IUserProfileRepository userProfileRepository)
        {
            _repository=repository;
            _db=db;
            _userProfileRepository=userProfileRepository;
        }

        public IAppointmentRepository appointmentRepository { get { return _repository; } }
        public IUserProfileRepository userProfileRepository { get { return _userProfileRepository; } }

        public async Task<bool> Save() => await _db.SaveChangesAsync() > 0 ? true : false;
    }
}
