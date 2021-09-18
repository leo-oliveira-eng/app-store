using FizzWare.NBuilder;
using IdentityServer.Domain.Models;
using IdentityServer.Domain.Services.Dtos;
using IdentityServer.Messaging.Enums;
using IdentityServer.Messaging.Requests;
using System;
using Valuables.Utils;

namespace IdentityServer.Application.Tests.Shared
{
    public class BaseMock
    {
        public CreateUserRequestMessage CreateUserRequestMessageFake(string name = null, string cpf = null, string password = null,
            string passwordConfirmation = null, string email = null, DateTime? birthDate = null, GenderType gender = default,
            CreateAddressRequestMessage address = null)
            => Builder<CreateUserRequestMessage>.CreateNew()
                .With(_ => _.Name, name ?? "Any Name")
                .With(_ => _.Cpf, cpf ?? "987.654.321-00")
                .With(_ => _.Password, password ?? "Abc123")
                .With(_ => _.PasswordConfirmation, passwordConfirmation ?? "Abc123")
                .With(_ => _.Email, email ?? "any@nothing.com")
                .With(_ => _.BirthDate, birthDate ?? DateTime.Today.AddYears(-20))
                .With(_ => _.Gender, gender == default ? GenderType.Other : gender)
                .With(_ => _.Address, address ?? CreateAddressRequestMessage())
                .Build();

        public  CreateAddressRequestMessage CreateAddressRequestMessage(string cep = null, string street = null, 
            string neighborhood = null, string number = null, string city = null, string uf = null, string complement = null)
            => Builder<CreateAddressRequestMessage>.CreateNew()
                .With(_ => _.Cep, cep ?? "22222-222")
                .With(_ => _.Street, street ?? "Rua do Passeio")
                .With(_ => _.Neighborhood, neighborhood ?? "Centro")
                .With(_ => _.Number, number ?? "38")
                .With(_ => _.City, city ?? "Rio de Janeiro")
                .With(_ => _.UF, uf ?? "RJ")
                .With(_ => _.Complement, complement)
                .Build();

        public CreateUserDto CreateUserDtoFake(string name = null, string cpf = null, string password = null,
            string passwordConfirmation = null, string email = null, DateTime? birthDate = null, Address address = null)
            => Builder<CreateUserDto>.CreateNew()
                .With(_ => _.Name, name ?? "Any Name")
                .With(_ => _.Cpf, cpf ?? "987.654.321-00")
                .With(_ => _.Password, password ?? "Abc123")
                .With(_ => _.PasswordConfirmation, passwordConfirmation ?? "Abc123")
                .With(_ => _.Email, email ?? "any@nothing.com")
                .With(_ => _.BirthDate, birthDate ?? DateTime.Today.AddYears(-20))
                .With(_ => _.Address, address ?? AddressFake())
                .Build();

        public User UserFake(CreateUserDto dto = null)
            => User.Create(dto ?? CreateUserDtoFake())
            .Data.Value;

        public Address AddressFake(string cep = null, string street = null, string neighborhood = null, string number = null, string city = null, string uf = null, string complement = null)
            => Address.Create(cep ?? "22222-222"
                , street ?? "Rua do Passeio"
                , neighborhood ?? "Centro"
                , number ?? "38"
                , city ?? "Rio de Janeiro"
                , uf ?? "RJ"
                , complement);

        public RecoverPasswordRequestMessage RecoverPasswordRequestMessageFake(string cpf = null)
            => Builder<RecoverPasswordRequestMessage>.CreateNew()
                .With(_ => _.Cpf, cpf ?? "987.654.321-00")
                .Build();
    }
}
