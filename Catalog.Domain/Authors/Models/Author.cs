using System;
using Valuables.Utils;

namespace Catalog.Domain.Authors.Models
{
    public class Author
    {
        public string Id { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public DateTime LastUpdate { get; private set; } = DateTime.Now;

        public DateTime? DeletedAt { get; private set; }

        public bool Deleted => DeletedAt.HasValue;
        public string Name { get; set; }

        public CNPJ CNPJ { get; set; }

        public string PhoneNumber { get; set; }

        public Email Email { get; set; }

        public Uri WebSite { get; set; }

        public string BrandLogo { get; set; }

        private Author() { }

        public Author(string name, CNPJ cnpj, string phoneNumber, Email email, Uri webSite, string brandLogo)
        {
            Name = name;
            CNPJ = cnpj;
            PhoneNumber = phoneNumber;
            Email = email;
            WebSite = webSite;
            BrandLogo = brandLogo;
        }
    }
}
