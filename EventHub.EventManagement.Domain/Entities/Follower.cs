using EventHub.EventManagement.Domain.Common;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;

namespace EventHub.EventManagement.Domain.Entities
{
   public class Follower : ISortableEntity, ISearchableEntity
   {
      public Guid FollowerId { get; set; }
      public string? FirstName { get; set; }
      public string? LastName { get; set; }
      public Genre? Genre { get; set; }
      public int Age { get; set; }
      public string? City { get; set; }
      public string? PhoneNumber { get; set; }
      public string? Email { get; set; }
      public ICollection<Producer> Producers { get; set; }
      public ICollection<Organization> Organizations { get; set; }

      public Follower()
      {
         Producers = new List<Producer>();
         Organizations = new List<Organization>();
      }
   }
}