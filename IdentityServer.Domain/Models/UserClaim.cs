using BaseEntity.Domain.Entities;
using Messages.Core;
using Messages.Core.Extensions;
using System;

namespace IdentityServer.Domain.Models
{
    public sealed class UserClaim : Entity
    {
        #region Properties

        public long UserId { get; private set; }

        public User User { get; private set; }

        public long ClaimId { get; private set; }

        public Claim Claim { get; private set; }

        #endregion

        #region Constructors

        [Obsolete(ConstructorObsoleteMessage, true)]
        UserClaim() : base(Guid.NewGuid()) { }

        UserClaim(User user, Claim claim) : base(Guid.NewGuid())
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            UserId = user.Id;
            Claim = claim ?? throw new ArgumentNullException(nameof(claim));
            ClaimId = claim.Id;
        }

        #endregion

        #region Methods

        public static Response<UserClaim> Create(User user, Claim claim)
        {
            var response = Response<UserClaim>.Create();

            var validationResponse = ValidateUserClaim(user, claim);

            if (validationResponse.HasError)
                return response.WithMessages(validationResponse.Messages);

            return response.SetValue(new UserClaim(user, claim));
        }

        private static Response ValidateUserClaim(User user, Claim claim)
        {
            var response = Response.Create();

            if (user is null)
                response.WithBusinessError(nameof(user), $"{nameof(user)} is required.");

            if (claim is null)
                response.WithBusinessError(nameof(claim), $"{nameof(claim)} is required.");

            return response;
        }

        #endregion

        #region Conversion Operators

        public static implicit operator UserClaim(Maybe<UserClaim> entity) => entity.Value;

        public static implicit operator UserClaim(Response<UserClaim> entity) => entity.Data;

        #endregion
    }
}
