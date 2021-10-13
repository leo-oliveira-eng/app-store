using Messages.Core;
using Messages.Core.Extensions;
using System;

namespace Catalog.Domain.Apps.Commands
{
    public class CreateAppCommand : AppCommand
    {
        public Guid AuthorID { get; set; }

        public override Response Validate()
        {
            var response = base.Validate();

            if (AuthorID.Equals(Guid.Empty))
                response.WithBusinessError(nameof(AuthorID), $"{nameof(AuthorID)} is invalid");

            return response;
        }
    }
}
