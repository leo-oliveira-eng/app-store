using IdentityServer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.Mappings
{
    public class ClientScopeMapping : IEntityTypeConfiguration<ClientScope>
    {
        public void Configure(EntityTypeBuilder<ClientScope> builder)
        {
            builder.ToTable(nameof(ClientScope));

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Scope).WithMany(x => x.Clients).HasForeignKey(x => x.ScopeId);

            builder.HasOne(x => x.Client).WithMany(x => x.Scopes).HasForeignKey(x => x.ClientId);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
