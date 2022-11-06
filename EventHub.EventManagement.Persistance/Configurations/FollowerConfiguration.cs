using EventHub.EventManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.EventManagement.Persistance.Configurations
{
   internal sealed class FollowerConfiguration : IEntityTypeConfiguration<Follower>
   {
      public void Configure(EntityTypeBuilder<Follower> builder)
      {
         builder
            .ToTable("Followers")
            .HasKey(p => p.FollowerId);






      }
   }
}
