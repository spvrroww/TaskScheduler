using Business.Repository;
using Business.Repository.IRepository;
using DataAccess;
using Common;
using Microsoft.AspNetCore.Identity;

namespace AppointmentScheduler.Services
{ // A repository manager service for all repositories
    public class RepositoryService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ApplicationDbContext _db;


        public RepositoryService(IAppointmentRepository repository, ApplicationDbContext db, IUserProfileRepository userProfileRepository)
        {
            _repository = repository;
            _db = db;
            _userProfileRepository = userProfileRepository;
        }

        public IAppointmentRepository appointmentRepository { get { return _repository; } }
        public IUserProfileRepository userProfileRepository { get { return _userProfileRepository; } }

        public async Task<bool> Save() => await _db.SaveChangesAsync() > 0 ? true : false;
        public void CreateDB()
        {
            _db.Database.EnsureCreated();
            if (_db.Roles.Any()) return; // If roles already exist, do not recreate them
            _db.Roles.Add(new IdentityRole { Name = SD.role_Customer, NormalizedName = SD.role_Customer.ToUpper() });
            _db.Roles.Add(new IdentityRole { Name = SD.role_Admin, NormalizedName = SD.role_Admin.ToUpper() });
            _db.SaveChanges();
        }
    }
}
