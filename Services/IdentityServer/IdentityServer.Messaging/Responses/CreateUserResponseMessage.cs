using Http.Request.Service.Messages;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IdentityServer.Messaging.Responses
{
    [DataContract]
    public class CreateUserResponseMessage : ResponseMessage
    {
        [DataMember]
        public Guid Code { get; set; }
        [DataMember]

        public string Identification { get; set; }
        [DataMember]

        public string Name { get; set; }
        [DataMember]

        public string Email { get; set; }

        [DataMember]
        public List<ClaimResponseMessage> Claims { get; set; }
    }
}
