using BaseEntity.Domain.Entities;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Collections.Generic;

namespace IdentityServer.Domain.Models
{
    public sealed class Claim : Entity
    {
        #region Properties

        public string Type { get; private set; }

        public string Value { get; private set; }

        public ICollection<UserClaim> Users { get; set; } = new HashSet<UserClaim>();

        public ICollection<ClientClaim> Clients { get; private set; } = new HashSet<ClientClaim>();

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, true)]
        Claim() : base(Guid.NewGuid()) { }

        Claim(Guid code, string type, string value) : base(code)
        {
            Type = type;
            Value = value;
        }

        #endregion

        #region Methods

        public static Response<Claim> Create(string type, string value)
        {
            var response = Response<Claim>.Create();

            var claimIsValidResponse = ClaimIsValid(type, value);

            if (claimIsValidResponse.HasError)
                return response.WithMessages(claimIsValidResponse.Messages);

            return response.SetValue(new Claim(Guid.NewGuid(), type, value));
        }

        private static Response ClaimIsValid(string type, string value)
        {
            var response = Response.Create();

            if (string.IsNullOrEmpty(type))
                response.WithBusinessError($"{nameof(type)} is required.");

            if (string.IsNullOrEmpty(value))
                response.WithBusinessError($"{nameof(value)} is required.");

            return response;
        }

        #endregion

        #region Conversion Operators

        public static implicit operator Claim(Maybe<Claim> entity) => entity.Value;

        public static implicit operator Claim(Response<Claim> entity) => entity.Data;

        #endregion
    }
}
