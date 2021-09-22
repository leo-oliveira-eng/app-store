using Http.Request.Service.Messages;
using IdentityServer.Messaging.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IdentityServer.Messaging.Requests
{
    [DataContract]
    public class CreateUserRequestMessage : RequestMessage
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Cpf { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string PasswordConfirmation { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public DateTime BirthDate { get; set; }

        [DataMember]
        public GenderType Gender { get; set; }

        [DataMember]
        public CreateAddressRequestMessage Address { get; set; }

        [DataMember]
        public List<CreateClaimRequestMessage> Claims { get; set; } = new List<CreateClaimRequestMessage>();
    }
}
