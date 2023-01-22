using EventHub.EventManagement.Domain.Common;
using EventHub.EventManagement.Domain.Entities.UserEntities;

namespace EventHub.EventManagement.Domain.Entities.ProducerEntities
{

    public class Producer : ISortableEntity, ISearchableEntity
   {
      public Guid ProducerId { get; set; }
      public string? FirstName { get; set; }
      public string? LastName { get; set; }
      public Genre? Genre { get; set; }
      public int Age { get; set; }
      public string? City { get; set; }
      public string? PhoneNumber { get; set; }
      public string? Email { get; set; }
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
