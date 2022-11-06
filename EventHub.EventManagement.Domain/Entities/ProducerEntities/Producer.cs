using EventHub.EventManagement.Domain.Common;

namespace EventHub.EventManagement.Domain.Entities.ProducerEntities
{

   public class Producer : User, ISortableEntity, ISearchableEntity
   {
      public Guid ProducerId { get; set; }
      public string? JobTitle { get; set; }
      public string? Bio { get; set; }
      public ICollection<Follower> Followers { get; set; }
      public ICollection<ProducerEvent> Events { get; set; }

      public Producer()
      {
         Followers = new List<Follower>();
         Events = new List<ProducerEvent>();
      }

   }


}
