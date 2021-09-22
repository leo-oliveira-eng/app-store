using System;

namespace IdentityServer.Domain.Services.Dtos
{
    public class ChangePasswordDto
    {
        public Guid PasswordRecoverCode { get; set; }

        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }
    }
}
