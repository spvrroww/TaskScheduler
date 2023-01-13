using AutoMapper;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<UserProfile, UserProfileDTO>().ReverseMap();
           
        }
    }
}
