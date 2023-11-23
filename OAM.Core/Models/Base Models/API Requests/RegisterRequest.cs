using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.Models.Base_Models.API_Requests
{
    [DataContract]
    public class RegisterRequest
    {
        [Required]
        [DataMember(Order=1)]
        public required string Name { get; set; }

        [Required]
        [DataMember(Order = 2)]
        public string Email { get; set; }

        [Required]
        [DataMember(Order = 3)]
        public string Password { get; set; }
    }
}
