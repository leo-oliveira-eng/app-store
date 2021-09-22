using System;

namespace IdentityServer.Domain.Events.Models
{
    public class RecoverPasswordEventModel : EventModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public Guid PasswordRecoverCode { get; set; }

        public DateTime RecoverPasswordExpirationDate { get; set; }
    }
}
