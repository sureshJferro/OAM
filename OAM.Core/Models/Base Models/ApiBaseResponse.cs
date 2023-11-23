using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.Models.Base_Models
{
    [DataContract]
    public record class ApiBaseResponse
    {
       
        [DataMember(Order =1)]
        public int StatusCode { get; set; }
      
        [DataMember(Order = 2)]
        public string Status { get; set; }

        [DataMember(Order = 3)]
        public string Message { get; set; }
    }
}
