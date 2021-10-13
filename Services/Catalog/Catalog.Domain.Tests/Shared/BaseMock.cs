using Catalog.Domain.Apps.Commands;
using Catalog.Domain.Authors.Commands;
using Catalog.Domain.Authors.Models;
using FizzWare.NBuilder;
using System;
using System.Collections.Generic;

namespace Catalog.Domain.Tests.Shared
{
    public class BaseMock
    {
        public CreateAuthorCommand CreateAuthorCommandFake(string name = null, string cnpj = null, string phoneNumber = null,
            string email = null, string webSite = null, string brandLogo = null)
            => Builder<CreateAuthorCommand>.CreateNew()
                .With(x => x.Name, name ?? "Any Name")
                .With(x => x.CNPJ, cnpj ?? "61.870.861/0001-15")
                .With(x => x.PhoneNumber, phoneNumber ?? "+55-11-99999 1234")
                .With(x => x.Email, email ?? "any@nothing.com")
                .With(x => x.WebSite, webSite ?? "https://anywebsite.com")
                .With(x => x.BrandLogo, brandLogo ?? "https://wheremylogois.com/123455.png")
                .Build();

        public UpdateAuthorCommand UpdateAuthorCommandFake(string phoneNumber = null, string email = null, string webSite = null,
            string brandLogo = null)
            => Builder<UpdateAuthorCommand>.CreateNew()
                .With(x => x.PhoneNumber, phoneNumber ?? "+55-11-99999 1234")
                .With(x => x.Email, email ?? "another@nothing.com")
                .With(x => x.WebSite, webSite ?? "https://anotherwebsite.com")
                .With(x => x.BrandLogo, brandLogo ?? "https://wheremylogois.com/123455.png")
                .Build();

        public Author AuthorFake()
            => Builder<Author>.CreateNew().Build();

        public DeleteAuthorCommand DeleteAuthorCommandFake(Guid? id = null)
            => new DeleteAuthorCommand(id ?? Guid.NewGuid());

        public CreateAppCommand CreateAppCommandFake(string name = null, string title = null, int? size = null, string version = null,
            string externalId = null, DateTime? releaseDate = null, string appLogo = null, List<string> languageList = null,
            decimal? price = null, Guid? authorId = null)
            => Builder<CreateAppCommand>.CreateNew()
                .With(x => x.Name, name ?? "Any name")
                .With(x => x.Title, title ?? "Any title")
                .With(x => x.Size, size ?? 333)
                .With(x => x.Version, version ?? "Latest")
                .With(x => x.ExternalId, externalId ?? "Any Id")
                .With(x => x.ReleaseDate, releaseDate ?? DateTime.Now.AddDays(-1))
                .With(x => x.AppLogo, appLogo ?? "whereismylogo.com.br")
                .With(x => x.LanguageList, languageList ?? new List<string> { "pt-br" })
                .With(x => x.Price, price ?? 1M)
                .With(x => x.AuthorID, authorId ?? Guid.NewGuid())
                .Build();
    }
}
