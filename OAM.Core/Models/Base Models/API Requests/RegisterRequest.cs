using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OAM.Core.Models.Base_Models.API_Requests
{
    [DataContract]
    public class RegisterRequest
    {
        public int Id { get; set; } 

        [Required]
        [DataMember(Order=1)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Name must contain only letters and numbers.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string UserName { get; set; }

        [Required]
        [DataMember(Order = 2)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email address must be at most 100 characters.")]
        public string Email { get; set; }

        [Required]
        [DataMember(Order = 3)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$", ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [Required]
        [DataMember(Order = 4)]
        public string PhoneNumber { get; set; }
    }
}
