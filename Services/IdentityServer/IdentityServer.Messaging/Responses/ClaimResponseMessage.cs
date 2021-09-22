using Http.Request.Service.Messages;
using System;
using System.Runtime.Serialization;

namespace IdentityServer.Messaging.Responses
{
    [DataContract]
    public class ClaimResponseMessage : ResponseMessage
    {
        [DataMember]
        public Guid Code { get; set; }
        
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}
