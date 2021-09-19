using IdentityServer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.Mappings
{
    public class ResourceMapping : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable(nameof(Resource));

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
