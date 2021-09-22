using Catalog.Messages.Requests;
using Catalog.Messages.Responses;
using Messages.Core;
using System.Threading.Tasks;

namespace Catalog.Application.Authors.Contracts
{
    public interface IAuthorApplicationService
    {
        Task<Response<CreateAuthorResponseMessage>> CreateAsync(CreateAuthorRequestMessage requestMessage);

        Task<Response<UpdateAuthorResponseMessage>> UpdateAsync(UpdateAuthorRequestMessage requestMessage);
    }
}
