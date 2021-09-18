using Http.Request.Service.Messages;
using System.Runtime.Serialization;

namespace IdentityServer.Messaging.Requests
{
    [DataContract]
    public class CreateClaimRequestMessage : RequestMessage
    {
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}
