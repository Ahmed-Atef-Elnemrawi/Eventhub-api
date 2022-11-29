using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.EventManagement.Persistance.Configurations
{
   internal sealed class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
   {
      public void Configure(EntityTypeBuilder<IdentityRole> builder)
      {
         builder.HasData(
            new IdentityRole
            {
               Name = "Manager",
               NormalizedName = "MANAGER"
            },
            new IdentityRole
            {
               Name = "Administrator",
               NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
               Name = "Producer",
               NormalizedName = "PRODUCER"
            },
            new IdentityRole
            {
               Name = "Organization",
               NormalizedName = "ORGANIZATION"
            });
      }
   }
}
