using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.Enums
{
    public class Enums
    {
        public enum StatusCode
        {
            [Display(Name = "Ok")]
            SUCCESS = 200,
            MOVED = 301,
            REDIRECT = 302,
            [Display(Name = "Bad Request")]
            Bad_Request = 400
        }
        public enum StatusMessage
        {
            [Display(Name = "Successful API Call")]
            Success,
            [Display(Name = "Bad Request")]
            Bad_Request,
            [Display(Name = "Saved Successfully")]
            Saved,
            [Display(Name = "Updated Successfully")]
            Updated,
            [Display(Name = "User Already Exists")]
            DuplicateUser

        }
    }
}
