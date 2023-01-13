using AutoMapper;
using Business.Repository.IRepository;
using DataAccess;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {

        public UserProfileRepository(ApplicationDbContext db): base(db)
        {
              
        }

     
    }
}
