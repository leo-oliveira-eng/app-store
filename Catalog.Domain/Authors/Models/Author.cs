using Catalog.Domain.Common;

namespace Catalog.Domain.Authors.Models
{
    public class Author : Entity
    {
        public string Name { get; set; }

        public string CNPJ { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string WebSite { get; set; }

        public string BrandLogo { get; set; }

        private Author() { }

        public Author(string name, string cnpj, string phoneNumber, string email, string webSite, string brandLogo)
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
