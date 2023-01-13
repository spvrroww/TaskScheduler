using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Helper.Specifications;
using DataAccess.Data;

namespace Business.Helper.Specification.AppointmentSpecifications
{
    public class GetAppointmentsByUserByDateSpecification : Specification<Appointment>
    {
      public GetAppointmentsByUserByDateSpecification(int profileId):base(x=>x.Id.Equals(profileId))
        {
            AddOrderByDescending(X => X.DateCreated);
        }

    }
}
