using BaseEntity.Domain.Events;
using Catalog.Domain.Authors.Models;

namespace Catalog.Domain.Authors.Events
{
    public class AuthorCreatedEvent : DomainEvent
    {
        public AuthorCreatedEvent(Author model)
            : base(model.Id)
        {
            Author = model;
        }

        public Author Author { get; set; }
    }
}
