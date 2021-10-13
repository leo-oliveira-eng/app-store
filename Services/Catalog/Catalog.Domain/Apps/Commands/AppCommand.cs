using BaseEntity.Domain.Messaging;
using Catalog.Domain.Apps.Models;
using MediatR;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Catalog.Domain.Apps.Commands
{
    public class AppCommand : Command, IRequest<Response<App>>
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public int Size { get; set; }

        public string Version { get; set; }

        public string ExternalId { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string AppLogo { get; set; }

        public List<string> LanguageList { get; set; }

        public decimal Price { get; set; }

        public override Response Validate()
        {
            var response = Response.Create();

            if (string.IsNullOrWhiteSpace(Name))
                response.WithBusinessError(nameof(Name), $"{nameof(Name)} is invalid");

            if (string.IsNullOrWhiteSpace(Title))
                response.WithBusinessError(nameof(Title), $"{nameof(Title)} is invalid");

            if (ReleaseDate.Equals(default))
                response.WithBusinessError(nameof(ReleaseDate), $"{nameof(ReleaseDate)} is invalid");

            if (string.IsNullOrWhiteSpace(Version))
                response.WithBusinessError(nameof(Version), $"{nameof(Version)} is invalid");

            if (Size.Equals(default))
                response.WithBusinessError(nameof(Size), $"{nameof(Size)} is invalid");

            if (Price.Equals(default))
                response.WithBusinessError(nameof(Price), $"{nameof(Price)} is invalid");

            return response;
        }
    }
}
