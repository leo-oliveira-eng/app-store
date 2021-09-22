using Http.Request.Service.Messages;
using System.Runtime.Serialization;

namespace Catalog.Messages.Requests
{
    [DataContract]
    public class CreateAuthorRequestMessage : RequestMessage
    {
        public string Name { get; set; }

        public string Cnpj { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string WebSite { get; set; }

        public string BrandLogo { get; set; }
    }
}
