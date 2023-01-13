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
     public static class MappingExtensions
    {

        public static AppointmentDTO MapToAppointmentDTO(this Appointment appointment, IMapper mapper) => mapper.Map<AppointmentDTO>(appointment);
        public static Appointment MapToAppointment(this AppointmentDTO appointmentDto, IMapper mapper) => mapper.Map<Appointment>(appointmentDto);
        public static UserProfileDTO MapToUserProfileDTO(this UserProfile userProfile, IMapper mapper) => mapper.Map<UserProfileDTO>(userProfile);
        public static UserProfile MapToUserProfile(this UserProfileDTO userProfile, IMapper mapper) => mapper.Map<UserProfile>(userProfile);


    }
}
