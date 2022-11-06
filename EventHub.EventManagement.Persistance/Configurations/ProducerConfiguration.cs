using EventHub.EventManagement.Domain.Entities.ProducerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.EventManagement.Persistance.Configurations
{
   internal sealed class ProducerConfiguration : IEntityTypeConfiguration<Producer>
   {
      public void Configure(EntityTypeBuilder<Producer> builder)
      {

         builder
            .ToTable("Producers")
           .HasKey(p => p.ProducerId);


         builder
            .HasMany(p => p.Followers)
            .WithMany(f => f.Producers)
            .UsingEntity<ProducerFollower>()
            .ToTable("ProducersFollowers")
            .HasKey(pf => new { pf.ProducerId, pf.FollowerId });



      }
   }
}
