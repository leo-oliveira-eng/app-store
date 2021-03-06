using Http.Request.Service.Messages;
using System;
using System.Runtime.Serialization;

namespace Catalog.Messages.Responses
{
    [DataContract]
    public class AuthorResponseMessage : ResponseMessage
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Cnpj { get; set; }

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
