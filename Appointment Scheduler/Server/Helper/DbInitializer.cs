using Appointment_Scheduler.Server.Helper.IHelper;
using Common;
using DataAccess;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Scheduler.Server.Helper
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ILogger<DbInitializer> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ILogger<DbInitializer> logger, ApplicationDbContext db, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger=logger;
            _db=db;
            _userManager=userManager;
            _roleManager=roleManager;
        }

        public  void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }

               
                if (_roleManager.Roles.Any()) return;
               

                _roleManager.CreateAsync(new IdentityRole(SD.role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.role_Customer)).GetAwaiter().GetResult();

                var result = _userManager.CreateAsync(new AppUser
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin@gmail.com"
                }).GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    var admin = _userManager.FindByEmailAsync("Admin@gmail.com").GetAwaiter().GetResult();
                    var roleResult = _userManager.AddToRoleAsync(admin, SD.role_Admin).GetAwaiter().GetResult();

                }



            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
