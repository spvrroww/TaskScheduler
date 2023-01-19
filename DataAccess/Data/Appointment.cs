using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public int profileId { get; set; }
        [ForeignKey(nameof(profileId))]
        public virtual UserProfile Profile { get; set; }

    }
}
