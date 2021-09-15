using BaseEntity.Domain.Entities;
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

        public Guid? PasswordRecoverCode { get; private set; }

        public DateTime? ExpirationDate { get; private set; }

        public ICollection<UserClaim> Claims { get; set; } = new HashSet<UserClaim>();

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, true)]
        User() : base(Guid.NewGuid()) { }

        User(CPF cpf, string name, string password, Email email) : base(Guid.NewGuid())
        {
            CPF = cpf;
            Name = name;
            Password = password;
            Email = email;
        }

        #endregion

        #region Methods

        public static Response<User> Create(string cpf, string name, string password, string email)
        {
            var response = Response<User>.Create();

            var userIsValidResponse = UserIsValid(cpf, name, password, email);

            if (userIsValidResponse.HasError)
                return response.WithMessages(userIsValidResponse.Messages);

            return response.SetValue(new User(CPF.Create(cpf), name, password.Sha256(), Email.Create(email)));
        }

        private static Response UserIsValid(string cpf, string name, string password, string email)
        {
            var response = Response.Create();

            if (!CPF.IsValid(cpf))
                response.WithBusinessError(nameof(cpf), $"{nameof(cpf)} is invalid");

            if (string.IsNullOrWhiteSpace(name))
                response.WithBusinessError(nameof(name), $"{nameof(name)} is invalid");

            if (!PasswordIsValid(password))
                response.WithBusinessError(nameof(password), "Password must contain at least six characters, one uppercase letter, one lowercase letter and one number");

            if (!Email.IsValid(email))
                response.WithBusinessError(nameof(email), $"{nameof(email)} is invalid");

            return response;
        }

        private static bool PasswordIsValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            var regex = new Regex(PASSWORD_CHECK_EXPRESSION);

            return regex.IsMatch(password);
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
            ExpirationDate = DateTime.Now.AddHours(2);
        }

        public Response ChangePassword(Guid recoverPasswordCode, string password)
        {
            var response = Response.Create();

            var recoverPasswrodIsValidResponse = RecoverPasswordCodeIsValid(recoverPasswordCode);

            if (recoverPasswrodIsValidResponse.HasError)
                return response.WithMessages(recoverPasswrodIsValidResponse.Messages);

            if (!PasswordIsValid(password))
                return response.WithBusinessError(nameof(password), "Password must contain at least six characters, one uppercase letter, one lowercase letter and one number");

            Password = password.Sha256();

            PasswordRecoverCode = null;

            ExpirationDate = null;

            return response;
        }

        private Response RecoverPasswordCodeIsValid(Guid recoverPasswordCode)
        {
            var response = Response.Create();

            if (!PasswordRecoverCode.HasValue || !PasswordRecoverCode.Equals(recoverPasswordCode))
                return response.WithBusinessError("Code is invalid.");

            if (ExpirationDate < DateTime.Now)
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
