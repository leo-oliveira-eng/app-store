using Catalog.Messages.Requests;
using Catalog.Messages.Responses;
using Messages.Core;
using System;
using System.Threading.Tasks;

namespace Catalog.Application.Authors.Contracts
{
    public interface IAuthorApplicationService
    {
        Task<Response<AuthorResponseMessage>> CreateAsync(CreateAuthorRequestMessage requestMessage);

        Task<Response<AuthorResponseMessage>> UpdateAsync(UpdateAuthorRequestMessage requestMessage, Guid id);

        Task<Response<AuthorResponseMessage>> FindAsync(Guid id);

        Task<Response<DeleteAuthorResponseMessage>> DeleteAsync(Guid id);
    }
}
