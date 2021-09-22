using BaseEntity.Domain.Messaging;
using Catalog.Domain.Authors.Models;
using MediatR;
using Messages.Core;
using Messages.Core.Extensions;
using System;

namespace Catalog.Domain.Authors.Commands
{
    public class AuthorCommand : Command, IRequest<Response<Author>>
    {
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string WebSite { get; set; }

        public string BrandLogo { get; set; }

        public override Response Validate()
        {
            var response = Response.Create();

            if (!Valuables.Utils.Email.IsValid(Email))
                response.WithBusinessError(nameof(Email), $"{nameof(Email)} is invalid");

            if (string.IsNullOrWhiteSpace(PhoneNumber))
                response.WithBusinessError(nameof(PhoneNumber), $"{nameof(PhoneNumber)} is invalid");

            if (string.IsNullOrWhiteSpace(WebSite) || !Uri.TryCreate(WebSite, UriKind.Absolute, out _))
                response.WithBusinessError(nameof(WebSite), $"{nameof(WebSite)} is invalid");

            if (string.IsNullOrWhiteSpace(BrandLogo) || !Uri.TryCreate(BrandLogo, UriKind.Absolute, out _))
                response.WithBusinessError(nameof(BrandLogo), $"{nameof(BrandLogo)} is invalid");

            return response;
        }
    }
}
