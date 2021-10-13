using Catalog.Messages.Requests;
using Catalog.Messages.Responses;
using Messages.Core;
using System.Threading.Tasks;

namespace Catalog.Application.Apps.Contracts
{
    public interface IAppApplicationService
    {
        Task<Response<AppResponseMessage>> CreateAsync(CreateAppRequestMessage requestMessage);
    }
}
