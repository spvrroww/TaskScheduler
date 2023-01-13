using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Registration
{
    public class RegistrationDTO
    {
        [Required(ErrorMessage = "First Name field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email field is required"), DataType(DataType.EmailAddress, ErrorMessage = "Input a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field is required"),
            DataType(DataType.Password),
            MinLength(8, ErrorMessage = "Password cannot be less than 8 characters and must inlude numeric characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password field is required"),
            DataType(DataType.Password),
            MinLength(8, ErrorMessage = "Password cannot be less than 8 characters and must inlude numeric characters"),
            Compare(nameof(Password), ErrorMessage = "Password don't Match, use correct password")]
        public string ConfirmPassword { get; set; }
    }
}
