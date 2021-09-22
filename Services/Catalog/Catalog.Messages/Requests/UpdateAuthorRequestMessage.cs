using Http.Request.Service.Messages;
using System;
using System.Runtime.Serialization;

namespace Catalog.Messages.Requests
{
    [DataContract]
    public class UpdateAuthorRequestMessage : RequestMessage
    {
        [DataMember]
        public Guid Code { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string WebSite { get; set; }

        [DataMember]
        public string BrandLogo { get; set; }
    }
}
