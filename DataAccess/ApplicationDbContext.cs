using DataAccess.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
        public ApplicationDbContext( DbContextOptions options): base(options)
        {

        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
    }
}
