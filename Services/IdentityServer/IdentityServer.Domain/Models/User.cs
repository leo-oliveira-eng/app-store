using BaseEntity.Domain.Entities;
using IdentityServer.Domain.Enums;
using IdentityServer.Domain.Services.Dtos;
using IdentityServer4.Models;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Valuables.Utils;

namespace IdentityServer.Domain.Models
{
    public class User : Entity
    {
        private const string PASSWORD_CHECK_EXPRESSION = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$";
        #region Properties

        public CPF CPF { get; private set; }

        public string Name { get; private set; }

        public string Password { get; private set; }

        public Email Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public GenderType Gender { get; private set; }

        public Address Address { get; private set; }

        public Guid? PasswordRecoverCode { get; private set; }

        public DateTime? RecoverPasswordExpirationDate { get; private set; }

        public ICollection<UserClaim> Claims { get; set; } = new HashSet<UserClaim>();

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, true)]
        User() : base(Guid.NewGuid()) { }

        User(CPF cpf, string name, string password, Email email, DateTime birthDate, GenderType gender, Address address)
            : base(Guid.NewGuid())
        {
            CPF = cpf;
            Name = name;
            Password = password;
            Email = email;
            BirthDate = birthDate;
            Gender = gender;
            Address = address;
        }

        #endregion

        #region Methods

        public static Response<User> Create(CreateUserDto dto)
        {
            var response = Response<User>.Create();

            var userIsValidResponse = UserIsValid(dto);

            if (userIsValidResponse.HasError)
                return response.WithMessages(userIsValidResponse.Messages);

            return response.SetValue(new User(CPF.Create(dto.Cpf), dto.Name, dto.Password.Sha256(),
                Email.Create(dto.Email), dto.BirthDate, dto.Gender, dto.Address));
        }

        private static Response UserIsValid(CreateUserDto dto)
        {
            var response = Response.Create();

            if (!CPF.IsValid(dto.Cpf))
                response.WithBusinessError(nameof(dto.Cpf), $"{nameof(dto.Cpf)} is invalid");

            if (string.IsNullOrWhiteSpace(dto.Name))
                response.WithBusinessError(nameof(dto.Name), $"{nameof(dto.Name)} is invalid");

            var passwordValidateResponse = PasswordIsValid(dto.Password, dto.PasswordConfirmation);

            if (passwordValidateResponse.HasError)
                response.WithMessages(passwordValidateResponse.Messages);

            if (!Email.IsValid(dto.Email))
                response.WithBusinessError(nameof(dto.Email), $"{nameof(dto.Email)} is invalid");

            if (!BirthDateIsValid(dto.BirthDate))
                response.WithBusinessError(nameof(dto.BirthDate), $"{nameof(dto.BirthDate)} is invalid.");

            if (!Enum.IsDefined(typeof(GenderType), dto.Gender))
                response.WithBusinessError(nameof(dto.Gender), $"{nameof(dto.Gender)} is invalid");

            if (dto.Address is null)
                response.WithBusinessError(nameof(dto.Address), $"{nameof(dto.Address)} is invalid");

            return response;
        }

        private static bool BirthDateIsValid(DateTime birthDate)
            => birthDate.Date <= DateTime.Today.AddYears(-18) && birthDate > DateTime.Today.AddYears(-120);

        private static Response PasswordIsValid(string password, string passwordConfirmation)
        {
            var response = Response.Create();

            if (string.IsNullOrWhiteSpace(password))
                return response.WithBusinessError(nameof(password), $"{nameof(password)} is invalid");

            if (!password.Equals(passwordConfirmation))
                return response.WithBusinessError(nameof(password), "Passwords don't match");

            var regex = new Regex(PASSWORD_CHECK_EXPRESSION);

            if (!regex.IsMatch(password))
                return response.WithBusinessError(nameof(password), "Password must contain at least six characters, one uppercase letter, one lowercase letter and one number");

            return response;
        }

        public Response AddClaim(Claim claim)
        {
            var response = Response.Create();

            if (claim != null && Claims.Any(c => c.Claim.Type.Equals(claim.Type) && c.Claim.Value.Equals(claim.Value)))
                return response;

            var userClaimCreate = UserClaim.Create(this, claim);

            if (userClaimCreate.HasError)
                return response.WithMessages(userClaimCreate.Messages);

            Claims.Add(userClaimCreate);

            return response;
        }

        public void GeneratePasswordRecoverCode()
        {
            PasswordRecoverCode = Guid.NewGuid();
            RecoverPasswordExpirationDate = DateTime.Now.AddHours(2);
        }

        public Response ChangePassword(Guid recoverPasswordCode, string password, string passwordConfirmation)
        {
            var response = Response.Create();

            var recoverPasswrodIsValidResponse = RecoverPasswordCodeIsValid(recoverPasswordCode);

            if (recoverPasswrodIsValidResponse.HasError)
                return response.WithMessages(recoverPasswrodIsValidResponse.Messages);

            var passwordValidateResponse = PasswordIsValid(password, passwordConfirmation);

            if (passwordValidateResponse.HasError)
                response.WithMessages(passwordValidateResponse.Messages);

            Password = password.Sha256();

            PasswordRecoverCode = null;

            RecoverPasswordExpirationDate = null;

            return response;
        }

        private Response RecoverPasswordCodeIsValid(Guid recoverPasswordCode)
        {
            var response = Response.Create();

            if (!PasswordRecoverCode.HasValue || !PasswordRecoverCode.Equals(recoverPasswordCode))
                return response.WithBusinessError("Code is invalid.");

            if (RecoverPasswordExpirationDate < DateTime.Now)
                return response.WithBusinessError("Code has expired.");

            return response;
        }

        #endregion

        #region Conversion Operators

        public static implicit operator User(Maybe<User> entity) => entity.Value;

        public static implicit operator User(Response<User> entity) => entity.Data;

        #endregion
    }
}
