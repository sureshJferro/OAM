using System.Runtime.Serialization;

namespace OAM.Core.Models.Base_Models.API_Requests
{
    [DataContract]
    public class LoginRequest
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
