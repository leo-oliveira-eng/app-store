using Messages.Core;
using Messages.Core.Extensions;

namespace Catalog.Domain.Authors.Commands
{
    public class CreateAuthorCommand : AuthorCommand
    {
        public string Name { get; set; }

        public string CNPJ { get; set; }

        public override Response Validate()
        {
            var response = Response.Create();

            if (string.IsNullOrWhiteSpace(Name))
                response.WithBusinessError(nameof(Name), $"{nameof(Name)} is invalid");

            if (!Valuables.Utils.CNPJ.IsValid(CNPJ))
                response.WithBusinessError(nameof(CNPJ), $"{nameof(CNPJ)} is invalid");
            
            var baseValidateResponse = base.Validate();

            if (baseValidateResponse.HasError)
                response.WithMessages(baseValidateResponse.Messages);

            return response;
        }
    }
}
