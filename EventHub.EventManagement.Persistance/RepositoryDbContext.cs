using EventHub.EventManagement.Domain.Common;
using EventHub.EventManagement.Domain.Entities;
using EventHub.EventManagement.Domain.Entities.EventEntities;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;
using EventHub.EventManagement.Persistance.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Presistence
{
   public sealed class RepositoryContext : IdentityDbContext<User>
   {
      public RepositoryContext(DbContextOptions<RepositoryContext> options)
         : base(options)
      { }


      public DbSet<Attendant>? Attendants { get; set; }
      public DbSet<Category>? Categories { get; set; }
      public DbSet<Event>? Events { get; set; }
      public DbSet<Follower>? Followers { get; set; }
      public DbSet<Medium>? Mediums { get; set; }
      public DbSet<Organization>? Organizations { get; set; }
      public DbSet<OrganizationEvent>? OrganizationEvents { get; set; }
      public DbSet<OrganizationEventSpeaker>? OrganizationsEventSpeakers { get; set; }
      public DbSet<OrganizationFollower>? OrganizationsFollowers { get; set; }
      public DbSet<Producer>? Producers { get; set; }
      public DbSet<ProducerEvent>? ProducerEvents { get; set; }
      public DbSet<ProducerFollower>? ProducersFollowers { get; set; }
      public DbSet<Speaker>? Speakers { get; set; }


      protected override void OnModelCreating(ModelBuilder builder)
      {
         builder.ApplyConfiguration(new AttendantConfiguration());
         builder.ApplyConfiguration(new ProducerConfiguration());
         builder.ApplyConfiguration(new OrganizationConfiguration());
         builder.ApplyConfiguration(new OrganizationEventConfiguration());
         builder.ApplyConfiguration(new FollowerConfiguration());
         builder.ApplyConfiguration(new CategoryConfiguration());
         builder.ApplyConfiguration(new SpeakerConfiguration());
         builder.ApplyConfiguration(new MediumConfiguration());
         builder.ApplyConfiguration(new RoleConfiguration());

         base.OnModelCreating(builder);

      }

      public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
      {
         foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
         {
            switch (entry.State)
            {
               case EntityState.Added:
                  entry.Entity.CreatedDate = DateTime.Now;
                  break;
               case EntityState.Modified:
                  entry.Entity.LastModifiedDate = DateTime.Now;
                  break;

            }
         }
         return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
      }
   }


}
