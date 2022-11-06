using EventHub.EventManagement.Domain.Common;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;

namespace EventHub.EventManagement.Domain.Entities
{
   public class Follower : User, ISortableEntity, ISearchableEntity
   {
      public Guid FollowerId { get; set; }
      public ICollection<Producer> Producers { get; set; }
      public ICollection<Organization> Organizations { get; set; }

      public Follower()
      {
         Producers = new List<Producer>();
         Organizations = new List<Organization>();
      }
   }
}