using Http.Request.Service.Messages;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Catalog.Messages.Responses
{
    [DataContract]
    public class AppResponseMessage : ResponseMessage
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Title { get; private set; }

        [DataMember]
        public int Size { get; private set; }

        [DataMember]
        public string Version { get; private set; }

        [DataMember]
        public string ExternalId { get; private set; }

        [DataMember]
        public DateTime ReleaseDate { get; private set; }

        [DataMember]
        public string AppLogo { get; private set; }

        [DataMember]
        public List<string> LanguageList { get; private set; }

        [DataMember]
        public decimal Price { get; private set; }

        [DataMember]
        public AuthorResponseMessage Author { get; private set; }
    }
}
