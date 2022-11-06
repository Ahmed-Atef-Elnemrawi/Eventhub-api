using EventHub.EventManagement.Domain.Entities.EventEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.EventManagement.Persistance.Configurations
{
   internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
   {
      public void Configure(EntityTypeBuilder<Category> builder)
      {
         builder
            .HasOne(c => c.Medium)
            .WithMany(m => m.Categories);


      }
   }
}
