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

      }
   }
}
