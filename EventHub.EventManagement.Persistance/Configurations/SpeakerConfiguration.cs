using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.EventManagement.Persistance.Configurations
{
   internal sealed class SpeakerConfiguration : IEntityTypeConfiguration<Speaker>
   {
      public void Configure(EntityTypeBuilder<Speaker> builder)
      {
         builder
            .ToTable("Speakers")
            .HasKey(p => p.SpeakerId);


      }
   }
}
