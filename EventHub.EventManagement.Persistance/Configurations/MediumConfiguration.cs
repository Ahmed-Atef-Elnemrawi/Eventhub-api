using EventHub.EventManagement.Domain.Entities.EventEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.EventManagement.Persistance.Configurations
{
   internal sealed class MediumConfiguration : IEntityTypeConfiguration<Medium>
   {
      public void Configure(EntityTypeBuilder<Medium> builder)
      {

         builder
            .HasMany(m => m.Categories)
            .WithOne(c => c.Medium)
            .HasPrincipalKey(m => m.MediumId)
            .HasForeignKey(c => c.MediumId);

         builder
            .HasData(new List<Medium>
            {
               new Medium
               {
                  MediumId = new Guid("07BA7806-DAD3-447F-8233-564FBC6C1010"),
                  Type=MediumType.None
               },

               new Medium
               {
                  MediumId = new Guid("FF1B224D-A32E-473B-9684-DF0493140094"),
                  Type = MediumType.Virtual
               },

               new Medium
               {
                  MediumId = new Guid("581B8AF3-81EB-4D29-865C-2686B6A0CE9B"),
                  Type = MediumType.Live
               },

               new Medium
               {
                  MediumId = new Guid("6691EFDB-7F06-4DA1-A31B-C29912C49AC4"),
                  Type = MediumType.Hybird
               }
            });
      }
   }
}
