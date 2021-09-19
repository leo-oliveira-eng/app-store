using IdentityServer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.Mappings
{
    public class ScopeResourceMapping : IEntityTypeConfiguration<ScopeResource>
    {
        public void Configure(EntityTypeBuilder<ScopeResource> builder)
        {
            builder.ToTable(nameof(ScopeResource));

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Scope).WithMany(x => x.Resources).HasForeignKey(x => x.ScopeId);

            builder.HasOne(x => x.Resource).WithMany(x => x.Scopes).HasForeignKey(x => x.ResourceId);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
