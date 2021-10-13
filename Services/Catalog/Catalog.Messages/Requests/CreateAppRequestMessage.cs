using Http.Request.Service.Messages;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Catalog.Messages.Requests
{
    [DataContract]
    public class CreateAppRequestMessage : RequestMessage
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int Size { get; set; }

        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public string ExternalId { get; set; }

        [DataMember]
        public DateTime ReleaseDate { get; set; }

        [DataMember]
        public string AppLogo { get; set; }

        [DataMember]
        public List<string> LanguageList { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public Guid AuthorId { get; set; }
    }
}
