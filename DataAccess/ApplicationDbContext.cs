using DataAccess.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApplicationDbContext> _logger;

        public ApplicationDbContext(DbContextOptions options, IConfiguration configuration, ILogger<ApplicationDbContext> logger) : base(options)
        {
            _configuration = configuration;
            _logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
            base.OnConfiguring(optionsBuilder);
            // You can add additional configuration here if needed
        }

        private string GetConnectionString()
        {
            // if (_configuration["ENVIRONMENT"].Equals("development", StringComparison.OrdinalIgnoreCase))
            // {
            //     _logger.LogInformation("Using dev environment variables for database connection string.");
            //     return _configuration.GetConnectionString("DefaultConnection");
            // }

            // _logger.LogInformation("Using Prod environment variables for database connection string.");

            // var dbName = _configuration["DB_NAME"];
            // var dbHost = _configuration["DB_HOST"];
            // var dbUsername = _configuration["DB_USERNAME"];
            // var dbPassword = _configuration["DB_PASSWORD"];

            // return $"Server={dbHost};Database={dbName};MultipleActiveResultSets=true;User Id={dbUsername};Password={dbPassword}";
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }

    }
}
