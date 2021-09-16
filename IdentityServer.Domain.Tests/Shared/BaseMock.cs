using FizzWare.NBuilder;
using IdentityServer.Domain.Enums;
using IdentityServer.Domain.Models;
using IdentityServer.Domain.Services.Dtos;
using System;
using Valuables.Utils;

namespace IdentityServer.Domain.Tests.Shared
{
    public class BaseMock
    {
        public Claim ClaimFake(string type = null, string value = null)
            => Claim.Create(type ?? "Any Type", value ?? "Any Value").Data.Value;

        public Email EmailFake(string address = null)
            => Email.Create(address ?? "any@nothing.com");

        public Address AddressFake(string cep = null, string street = null, string neighborhood = null, string number = null, string city = null, string uf = null, string complement = null)
            => Address.Create(cep ?? "22222-222"
                , street ?? "Rua do Passeio"
                , neighborhood ?? "Centro"
                , number ?? "38"
                , city ?? "Rio de Janeiro"
                , uf ?? "RJ"
                , complement);

        public CreateUserDto CreateUserDtoFake(string name = null, string cpf = null, string password = null, string email = null,
            DateTime? birthDate = null, GenderType gender = default, Address address = null)
            => Builder<CreateUserDto>.CreateNew()
                .With(_ => _.Name, name ?? "Any Name")
                .With(_ => _.Cpf, cpf ?? "987.654.321-00")
                .With(_ => _.Password, password ?? "Abc123")
                .With(_ => _.Email, email ?? "any@nothing.com")
                .With(_ => _.BirthDate, birthDate ?? DateTime.Today.AddYears(-20))
                .With(_ => _.Gender, gender == default ? GenderType.Other : gender)
                .With(_ => _.Address, address ?? AddressFake())
                .Build();

        public User UserFake(CreateUserDto dto = null)
            => User.Create(dto ?? CreateUserDtoFake())
            .Data.Value;
    }
}
