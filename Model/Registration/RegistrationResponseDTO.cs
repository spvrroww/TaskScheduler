using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Registration
{
    public class RegistrationResponseDTO
    {
        public UserProfileDTO? Profile { get; set; }
        public bool IsSuccessful { get; set; }
        public string? Error { get; set; }
    }
}
