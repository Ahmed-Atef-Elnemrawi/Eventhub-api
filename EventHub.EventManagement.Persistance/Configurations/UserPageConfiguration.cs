using EventHub.EventManagement.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.EventManagement.Persistance.Configurations
{
   public class UserPageConfiguration : IEntityTypeConfiguration<UserPage>
   {
      public void Configure(EntityTypeBuilder<UserPage> builder)
      {
         builder.ToTable("UsersPages");

      }
   }
}
