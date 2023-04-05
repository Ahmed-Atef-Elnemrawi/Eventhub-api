using EventHub.EventManagement.Domain.Entities.EventEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.EventManagement.Persistance.Configurations
{
   internal sealed class AttendantConfiguration : IEntityTypeConfiguration<Attendant>
   {
      public void Configure(EntityTypeBuilder<Attendant> builder)
      {
         builder
            .ToTable("Attendants")
            .HasKey(p => p.AttendantId);


         builder
            .HasMany(p => p.Events)
            .WithMany(p => p.Attendants)
            .UsingEntity<EventAttendant>()
            .ToTable("EventsAttendants")
            .HasKey(p => new { p.EventId, p.AttendantId });

      }
   }
}
