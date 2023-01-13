using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }

    }

}
