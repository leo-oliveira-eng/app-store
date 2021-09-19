using IdentityServer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.Mappings
{
    public class ClientClaimMapping : IEntityTypeConfiguration<ClientClaim>
    {
        public void Configure(EntityTypeBuilder<ClientClaim> builder)
        {
            builder.ToTable(nameof(ClientClaim));

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Claim).WithMany(x => x.Clients).HasForeignKey(x => x.ClaimId);

            builder.HasOne(x => x.Client).WithMany(x => x.Claims).HasForeignKey(x => x.ClientId);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
