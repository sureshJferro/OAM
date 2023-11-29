using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OAM.Core.Models.Base_Models.API_Responses
{
    [DataContract]
        public class ApiLogEntryResponse
        {
            [DataMember]
            public long ApiLogEntryId { get; set; }             // The (database) ID for the API log entry.
            [DataMember]
            public string Application { get; set; }             // The application that made the request.
            [DataMember]
            public string UserName { get; set; }                    // The user that made the request.
            [DataMember]
            public string Machine { get; set; }                 // The machine that made the request.
            [DataMember]
            public string RequestIpAddress { get; set; }        // The IP address that made the request.
            [DataMember]
            public string RequestContentType { get; set; }      // The request content type.
            [DataMember]
            public string RequestContentBody { get; set; }      // The request content body.
            [DataMember]
            public string RequestUri { get; set; }              // The request URI.
            [DataMember]
            public string RequestMethod { get; set; }           // The request method (GET, POST, etc).
            [DataMember]
            public string RequestRouteTemplate { get; set; }    // The request route template.
            [DataMember]
            public string RequestRouteData { get; set; }        // The request route data.
            [DataMember]
            public string RequestHeaders { get; set; }          // The request headers.
            [DataMember]
            public DateTime? RequestTimestamp { get; set; }     // The request timestamp.
            [DataMember]
            public string ResponseContentType { get; set; }     // The response content type.
            [DataMember]
            public string ResponseContentBody { get; set; }     // The response content body.
            [DataMember]
            public int? ResponseStatusCode { get; set; }        // The response status code.
            [DataMember]
            public string ResponseHeaders { get; set; }         // The response headers.
            [DataMember]
            public string ControllerName { get; set; }          // The request Controller name.
            [DataMember]
            public string ActionName { get; set; }              // The request ActionName.
            [DataMember]
            public long UserId { get; set; }                   // The request Header UserId.
            [DataMember]
            public long? SubUserId { get; set; }                   // The request Header SubUserId.
            [DataMember]
            public string ActivityLog { get; set; }
            [DataMember]
            public string ActionType { get; set; }
            [DataMember]
            public string Activity { get; set; }
            [DataMember]
            public string ActivityFrom { get; set; }
            [DataMember]
            public string Browser { get; set; }
            [DataMember]
            public DateTime? ResponseTimestamp { get; set; }    // The response timestamp.
            [DataMember]
            public int Sno { get; set; }
            [DataMember]
            public DateTime ActivityDate { get; set; }
            [DataMember]
            public int TotalRecordCount { get; set; }
            [DataMember]
            public string ClientIpAddress { get; set; }
            [DataMember]
            public string RequestAppId { get; set; }
            [DataMember]
            public string RequestActionMethod { get; set; }
            [DataMember]
            public string ResponseErrorMsg { get; set; }
            [DataMember]
            public string RequestUserToken { get; set; }
            [DataMember]
            public long APIRequestId { get; set; }
            [DataMember]
            public long ApiStatusId { get; set; }
            [DataMember]
            public int ElasticSearchRefId { get; set; }
            [DataMember]
            public bool IsDeleted { get; set; }
            [DataMember]
            public string APIType { get; set; }
            [DataMember]
            public Guid? ElasticId { get; set; }

        }

        [DataContract]
        public class ActivityInfo
        {
            [DataMember]
            public string ActionType { get; set; }
            [DataMember]
            public string ActivityLog { get; set; }
        }
    }

