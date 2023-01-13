using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Authentication
{
    public class AuthenticationResponseDTO
    {
        public string Token { get; set; }
        public bool IsSuccessful { get; set; }
        public  string Error { get; set; }
    }
}
