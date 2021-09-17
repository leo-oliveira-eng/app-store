using IdentityServer.Domain.Enums;
using System;
using System.Collections.Generic;
using Valuables.Utils;

namespace IdentityServer.Domain.Services.Dtos
{
    public class CreateUserDto
    {
        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public GenderType Gender { get; set; }

        public Address Address { get; set; }

        public List<ClaimDto> Claims { get; set; } = new List<ClaimDto>();
    }
}
