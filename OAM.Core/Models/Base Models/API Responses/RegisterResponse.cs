using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OAM.Core.Models.Base_Models.API_Responses
{

    public record RegisterResponse : ApiBaseResponse
    {
        public UserDetails userDetails { get; set; }
    }
    [DataContract]
    public record UserDetails
    {
        [JsonIgnore]
        public int Id { get; set; }

        [DataMember(Order = 1)]
        public Guid UserId { get; set; }
        
        [DataMember(Order = 2)]
        public string UserName { get; set; }

        [DataMember(Order = 3)]
        public string EmailAddress { get; set; }

        [DataMember(Order = 4)]
        public DateTime CreatedTime { get; set; }
    }
}
