using Http.Request.Service.Messages;
using System.Runtime.Serialization;

namespace IdentityServer.Messaging.Requests
{
    [DataContract]
    public class CreateAddressRequestMessage : RequestMessage
    {
        [DataMember]
        public string Cep { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string Complement { get; set; }

        [DataMember]
        public string Neighborhood { get; set; }

        [DataMember]
        public string Number { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string UF { get; set; }
    }
}
