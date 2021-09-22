using Catalog.Domain.Authors.Models;
using Catalog.Messages.Requests;
using FizzWare.NBuilder;

namespace Catalog.Application.Tests.Shared
{
    public class BaseMock
    {
        public CreateAuthorRequestMessage CreateAuthorRequestMessageFake()
            => Builder<CreateAuthorRequestMessage>.CreateNew().Build();

        public Author AuthorFake()
            => Builder<Author>.CreateNew().Build();

        public UpdateAuthorRequestMessage UpdateAuthorRequestMessageFake()
            => Builder<UpdateAuthorRequestMessage>.CreateNew().Build();
    }
}
