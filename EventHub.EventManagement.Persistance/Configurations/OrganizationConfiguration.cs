using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.EventManagement.Persistance.Configurations
{
   internal sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
   {
      public void Configure(EntityTypeBuilder<Organization> builder)
      {


         builder
            .HasMany(o => o.Followers)
            .WithMany(f => f.Organizations)
            .UsingEntity<OrganizationFollower>()
            .ToTable("OrganizationsFollowers")
            .HasKey(of => new { of.OrganizationId, of.FollowerId });

         builder
            .HasMany(o => o.Speakers)
            .WithOne(s => s.Organization)
            .HasPrincipalKey(o => o.OrganizationId)
            .HasForeignKey(s => s.OrganizationId);

      }
   }
}
