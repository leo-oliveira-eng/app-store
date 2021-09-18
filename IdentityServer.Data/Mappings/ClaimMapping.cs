using IdentityServer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.Mappings
{
    public class ClaimMapping : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.ToTable(nameof(Claim));

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
