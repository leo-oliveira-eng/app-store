using BaseEntity.Domain.Events;
using Catalog.Domain.Authors.Models;

namespace Catalog.Domain.Authors.Events
{
    public class UpdatedAuthorEvent : DomainEvent
    {
        public UpdatedAuthorEvent(Author model)
            : base(model.Code)
        {
            Author = model;
        }

        public Author Author { get; set; }
    }
}
