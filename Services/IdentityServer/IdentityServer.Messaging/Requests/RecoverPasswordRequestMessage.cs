using Http.Request.Service.Messages;
using System.Runtime.Serialization;

namespace IdentityServer.Messaging.Requests
{
    [DataContract]
    public class RecoverPasswordRequestMessage : ResponseMessage
    {
        [DataMember]
        public string Cpf { get; set; }
    }
}
