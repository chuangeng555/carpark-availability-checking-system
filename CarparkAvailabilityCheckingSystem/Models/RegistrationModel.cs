using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarparkAvailabilityCheckingSystem.Models
{
    public class RegistrationModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password more than 8 char")]
        public string Password { get; set; }

        [MinLength(8, ErrorMessage = "Number more than 8")]
        public string Contact { get; set; }
    }
}
