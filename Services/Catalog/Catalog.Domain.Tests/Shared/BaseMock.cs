using Catalog.Domain.Authors.Commands;
using Catalog.Domain.Authors.Models;
using FizzWare.NBuilder;

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
    }
}
