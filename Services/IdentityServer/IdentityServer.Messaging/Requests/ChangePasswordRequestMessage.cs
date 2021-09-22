using Http.Request.Service.Messages;
using System;
using System.Runtime.Serialization;

namespace IdentityServer.Messaging.Requests
{
    [DataContract]
    public class ChangePasswordRequestMessage : RequestMessage
    {
        [DataMember]
        public Guid PasswordRecoverCode { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string PasswordConfirmation { get; set; }
    }
}
