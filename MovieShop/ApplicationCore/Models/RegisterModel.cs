using ApplicationCore.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email can not be empty")]
        [EmailAddress(ErrorMessage = "Email address is not in a valid format")]
        [StringLength(100, ErrorMessage = "Email must be 100 characters or fewer")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password can not be empty")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and contain at least 1 uppercase letter, 1 lowercase letter, and 1 number.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "First name can not be empty")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name can not be empty")]

        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [YearValidation(1900, 2000)]
        public DateTime DateOfBirth { get; set; }

    }
}
