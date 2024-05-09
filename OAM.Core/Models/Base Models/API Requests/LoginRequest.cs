using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace OAM.Core.Models.Base_Models.API_Requests
{
    [DataContract]
    public class LoginRequest
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [JsonIgnore]
        public string? Email { get; set; }

        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }
    }
}
