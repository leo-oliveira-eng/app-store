using System;

namespace Catalog.Domain.Authors.Commands
{
    public class UpdateAuthorCommand : AuthorCommand
    {
        public Guid Code { get; set; }
    }
}
