using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.EventManagement.Persistance.Configurations
{
   internal sealed class OrganizationEventConfiguration : IEntityTypeConfiguration<OrganizationEvent>
   {
      public void Configure(EntityTypeBuilder<OrganizationEvent> builder)
      {
         builder
             .HasMany(e => e.Speakers)
             .WithMany(s => s.OrganizationEvents)
             .UsingEntity<OrganizationEventSpeaker>()
             .ToTable("OrganizationsEventsSpeakers")
             .HasKey(a => new { a.OrganizationEventId, a.SpeakerId });

      }
   }
}

